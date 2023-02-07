using PokeApiNet;
using Pokemon.Models;

namespace Pokemon.Managers
{
    public class PokemonManager : IPokemonManager
    {
        private readonly PokeApiClient _pokeClient;

        public PokemonManager(PokeApiClient pokeClient)
        {
            _pokeClient = pokeClient;
        }

        public async Task<PokemonOutPutModel> GetPokemonInformationAsync(string idOrName, string? culture)
        {
            var items = await _pokeClient.GetResourceAsync<PokeApiNet.Item>(idOrName);
            var dataToTranslate = string.IsNullOrWhiteSpace(culture) ? items.FlavorGroupTextEntries.FirstOrDefault() :
                items.FlavorGroupTextEntries.FirstOrDefault(x => x.Language.Name.Equals(culture));


            return new PokemonOutPutModel()
            {
                ShakespeareanDescription = "TODO",
                Language = dataToTranslate.Language.Name,
                FlavorText = dataToTranslate.Text,
                Version = dataToTranslate.VersionGroup.Name
            };
        }
    }
}
