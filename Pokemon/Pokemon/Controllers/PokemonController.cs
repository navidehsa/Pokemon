using Microsoft.AspNetCore.Mvc;
using Pokemon.Managers;

namespace Pokemon.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{

    private readonly IPokemonManager _pokemonManager;
    private readonly ILogger<PokemonController> _logger;
    

    public PokemonController(ILogger<PokemonController> logger, IPokemonManager pokemonManager)
    {
        _logger = logger;
        _pokemonManager = pokemonManager;
    }

    [HttpGet("{nameOrId}")]
    public async Task<IActionResult> GetPokemonAsync(string nameOrId, string? language)
    {
        var result= await _pokemonManager.GetPokemonInformationAsync(nameOrId, language);
        return Ok(result);
    }
}
