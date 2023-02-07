using Pokemon.Models;

namespace Pokemon.ShakespeareClient
{
    public interface IShakespeareClient
    {
        Task<ShakespeareOutputModel> GetTranslationAsync(string inputText);
    }
}
