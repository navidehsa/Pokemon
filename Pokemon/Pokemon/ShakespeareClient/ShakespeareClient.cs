using Pokemon.Models;
using Pokemon.RestClient;

namespace Pokemon.ShakespeareClient
{
    public class ShakespeareClient : IShakespeareClient
    {
        private const string _apiBaseUrl = "https://api.funtranslations.com/translate/shakespeare.json";
        //TODO move to appsettings
        private readonly IHttpRestClient _httpRestClient;

        public ShakespeareClient(IHttpRestClient httpRestClient)
        {
            _httpRestClient = httpRestClient;
        }

        public Task<ShakespeareOutputModel> GetTranslationAsync(string inputText)
        {

            return _httpRestClient.SendRequestAsync<ShakespeareInputModel, ShakespeareOutputModel>( new() { text = inputText },
                
                HttpMethod.Post, _apiBaseUrl, null);
            
        }

    }
}
