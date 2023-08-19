﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable enable

using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Core.Common.Errors;
using Shared.Contracts.Constants.Http;

namespace Core.Common.Errors;

internal sealed class CustomProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;
    private readonly Action<CustomProblemDetailsContext>? _configure;

    public CustomProblemDetailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        _configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });

        List<Error>? errorList = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;

        if(errorList is not null)
        {
            problemDetails.Extensions.Add("errorCodes", errorList.Select(e => e.Code));
        }
    }

    internal sealed class CustomProblemDetailsContext
    {
        private ProblemDetails? _problemDetails;

        /// <summary>
        /// The <see cref="HttpContext"/> associated with the current request being processed by the filter.
        /// </summary>
        public HttpContext HttpContext { get; init; } = null!;

        /// <summary>
        /// A collection of additional arbitrary metadata associated with the current request endpoint.
        /// </summary>
        public EndpointMetadataCollection? AdditionalMetadata { get; init; }

        /// <summary>
        /// An instance of <see cref="ProblemDetails"/> that will be
        /// used during the response payload generation.
        /// </summary>
        public ProblemDetails ProblemDetails
        {
            get => _problemDetails ??= new ProblemDetails();
            init => _problemDetails = value;
        }
    }
    internal sealed class ProblemDetailsOptions
    {
        /// <summary>
        /// The operation that customizes the current <see cref="Mvc.ProblemDetails"/> instance.
        /// </summary>
        public Action<CustomProblemDetailsContext>? CustomizeProblemDetails { get; set; }
    }
}



