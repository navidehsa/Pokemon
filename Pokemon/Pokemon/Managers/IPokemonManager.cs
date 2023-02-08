using Pokemon.Models;

namespace Pokemon.Managers
{
    public interface IPokemonManager
    {
        Task<PokemonOutPutModel> GetPokemonInformationAsync(string idOrName, string? language);
    }
}
