using Blazor.Extensions;
using Blazor.Infrastracture.Interceptors.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Shared.Abstraction.Interceptors;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace Blazor.Infrastracture.Managers.Http
{
    /// <summary>
    /// HttpClient owner / manager class as a response to not implemented yet
    ///     shared scopes between App DI scope and DelegetingHandler DI scope.
    /// </summary>
    public class HttpClientOwner
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpRequestInterceptor _httpRequestInterceptor;

        public HttpClientOwner(IHttpClientFactory httpClientFactory, IHttpRequestInterceptor httpRequestInterceptor)
        {
            _httpClient = httpClientFactory.CreateClient(WebAssemblyHostBuilderExtensions.HttpClientName);
            _httpRequestInterceptor = httpRequestInterceptor;
        }

        /// <summary>
        /// This method should be used only to manipulate the HttpClient configuration like default headers, etc.
        /// </summary>
        /// <returns>Returns wrapped owned httpClient</returns>
        public HttpClient GetWrappedHttpClient()
        {
            return _httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);

            _httpRequestInterceptor.InterceptAfterHttpRequest(response);

            return response;
        }
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
        {
            var response = await _httpClient.SendAsync(requestMessage);

            _httpRequestInterceptor.InterceptAfterHttpRequest(response);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string requestUri, TValue value)
        {
            var response = await _httpClient.PostAsJsonAsync(requestUri, value);

            _httpRequestInterceptor.InterceptAfterHttpRequest(response);

            return response;
        }
    }

}
