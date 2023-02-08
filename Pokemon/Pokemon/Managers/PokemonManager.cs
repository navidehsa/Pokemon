using PokeApiNet;
using Pokemon.Models;
using Pokemon.ShakespeareClient;

namespace Pokemon.Managers
{
    public class PokemonManager : IPokemonManager
    {
        private readonly PokeApiClient _pokeClient;
        private readonly IShakespeareClient _shakespeareClient;

        public PokemonManager(PokeApiClient pokeClient, IShakespeareClient shakespeareClient)
        {
            _pokeClient = pokeClient;
            _shakespeareClient = shakespeareClient;
        }

        public async Task<PokemonOutPutModel> GetPokemonInformationAsync(string idOrName, string? language)
        {
            var items = await _pokeClient.GetResourceAsync<PokeApiNet.Item>(idOrName);
            var dataToTranslate = string.IsNullOrWhiteSpace(language) ? items.FlavorGroupTextEntries.FirstOrDefault() :
                items.FlavorGroupTextEntries.FirstOrDefault(x => x.Language.Name.Equals(language));

            var translation = await _shakespeareClient.GetTranslationAsync(dataToTranslate.Text);

            return new PokemonOutPutModel()
            {
                ShakespeareanDescription = translation.contents.translated,
                Language = dataToTranslate.Language.Name,
                FlavorText = dataToTranslate.Text,
                Version = dataToTranslate.VersionGroup.Name
            };
        }
    }
}
