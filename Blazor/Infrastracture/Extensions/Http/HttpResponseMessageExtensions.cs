using System.Text.Json.Serialization;
using System.Text.Json;
using ErrorOr;
using Shared.Contracts.Constants.Http;

namespace Blazor.Infrastracture.Extensions.Http
{
    /// <summary>
    /// HttpResponseMessage extensions to deserialize the response from the server
    /// it will also check if response is an ProblemDetails or 
    /// desired success result to perform correct deserialization.
    /// </summary>
    internal static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Method to deserialize the response from the server
        /// </summary>
        /// <typeparam name="T">Generic type to return if response has successful status</typeparam>
        /// <param name="response">Response from server</param>
        /// <returns></returns>
        internal static async Task<ErrorOr<T>> ToResult<T>(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

            if (!response.IsSuccessStatusCode
                &&
                response.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(json, jsonOptions);
                
                if(problemDetails is null)
                {
                    return Error.Unexpected(); // to do: add error message and create it in reusable place
                }

                return RevertProblemDetailsToErrorList(problemDetails);
            }
           
            var result = JsonSerializer.Deserialize<T>(json, jsonOptions);

            if(result is null)
            {
                return Error.Unexpected(); // to do: add error message and create it in reusable place
            }

            return result;
        }
        /// <summary>
        /// Method to deserialize the response from the server, result-less overload (if response has successful status)
        /// </summary>
        /// <param name="response">Response from server</param>
        /// <returns></returns>
        internal static async Task<ErrorOr<Task>> ToResult(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode
                &&
                response.Content.Headers.ContentType?.MediaType == "application/problem+json")
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(json, jsonOptions);

                if (problemDetails is null)
                {
                    return Error.Unexpected(); // to do: add error message and create it in reusable place
                }

                return RevertProblemDetailsToErrorList(problemDetails);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Reverting problem details to initial state which is error list.
        /// </summary>
        /// <param name="problemDetails">Problem details received via deserializing response</param>
        /// <returns></returns>
        private static List<Error> RevertProblemDetailsToErrorList(ProblemDetails problemDetails)
        {
            List<Error> errors = new();

            ErrorType httpStatusCode = problemDetails.Status switch
            {
                StatusCodes.Status400BadRequest => ErrorType.Validation,
                StatusCodes.Status409Conflict => ErrorType.Conflict,
                StatusCodes.Status404NotFound => ErrorType.NotFound,
                _ => ErrorType.Unexpected
            };

            for(int i = 0; i < problemDetails.Errors.Count; i++)
            {
                errors.Add(Error.Custom((int)httpStatusCode,
                                        problemDetails.Errors.ElementAt(i).Key,
                                        String.Join('\n', problemDetails.Errors.ElementAt(i).Value)));
            }

            return errors;
        }
    }

    /// <summary>
    /// This problemDetails will be used only here to deserialize the response from the server
    /// and to avoid direct Microsoft.AspNetCore.Mvc package reference in the Blazor project
    /// </summary>
    internal sealed class ProblemDetails
    {
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Status { get; set; }
        public string TraceId { get; set; } = null!;
        public Dictionary<string, string[]> Errors { get; set; } = null!;

        [JsonExtensionData]
        public Dictionary<string, object> ExtensionData { get; set; } = null!;
    }

}
