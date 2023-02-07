namespace Pokemon.RestClient
{
    public interface IHttpRestClient
    {
        Task<HttpResponseMessage> SendRequestAsync<T>(T payload, HttpMethod method, string url, string token);
        Task<TResponse> SendRequestAsync<T, TResponse>(T payload, HttpMethod method, string url, string token);
    }
}
