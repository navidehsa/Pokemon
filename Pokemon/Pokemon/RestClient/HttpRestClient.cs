using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pokemon.RestClient
{
    public class HttpRestClient : IHttpRestClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
    

        public HttpRestClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// Send request to REST endpoint
        /// </summary>
        /// <param name="payload">The payload to send, null IS allowed</param>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendRequestAsync<T>(T payload, HttpMethod method, string url, string token)
        {
            var requestContent = SerializeRequestPayload(payload);
            var requestMessage = new HttpRequestMessage(method, url)
            {
                Method = method,
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var client= _httpClientFactory.CreateClient();
            var httpResponse = await client.SendAsync(requestMessage);
               
            if (!httpResponse.IsSuccessStatusCode)
            {
                await ThrowErrorAsync(httpResponse, requestMessage.RequestUri);
            }
            return httpResponse;

        }

        /// <summary>
        /// Send a request to a REST endpoint and parse the response payload into <typeparamref name="TResponse"/>
        /// </summary>
        /// <param name="payload">The request payload to send, null IS allowed!</param>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public async Task<TResponse> SendRequestAsync<T, TResponse>(T payload, HttpMethod method, string url, string token)
        {
            var httpResponse = await SendRequestAsync<T>(payload, method, url, token);

            if (httpResponse?.Content == null)
            {
                return default(TResponse);
            }

            var responseString = await httpResponse.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<TResponse>(responseString);
            return res;
        }

        private string SerializeRequestPayload<T>(T payload)
        {
            // no payload
            if (payload == null) return string.Empty;

            // payload already a string?
            if (payload is string strPayload) return strPayload;

            // serialize it into json
            return JsonSerializer.Serialize(payload);
        }
        private static async Task ThrowErrorAsync(HttpResponseMessage httpResponse, Uri? uri)
        {
            var responseBody = await httpResponse.Content.ReadAsStringAsync();

            var errorMsg =
                $"Failed to execute an HTTP request to '{uri?.AbsoluteUri ?? null}'. Response: {(int)httpResponse.StatusCode} {httpResponse.ReasonPhrase} {responseBody}.";

            throw new Exception(errorMsg);
        }
    }
}
