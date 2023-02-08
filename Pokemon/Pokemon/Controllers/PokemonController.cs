using Microsoft.AspNetCore.Mvc;
using Pokemon.Managers;
using Pokemon.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokemon.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonManager _pokemonManager;
    private readonly ILogger<PokemonController> _logger;

    //TODO 

    public PokemonController(ILogger<PokemonController> logger, IPokemonManager pokemonManager)
    {
        _logger = logger;
        _pokemonManager = pokemonManager;
    }

    [HttpGet("{nameOrId}")]
    [SwaggerOperation(
        Summary = "Fetches information about a specific Pokemon and returns its description in Shakespearean language.",
        Description = "It is possible to define the language, if the language is not defined, It use the first one in the list.",
        OperationId = "GetPokemon"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Request Successful", typeof(PokemonOutPutModel))]
    public async Task<IActionResult> GetPokemonAsync([SwaggerParameter(Description = "The name or Id of Pokemon", Required = true)] string nameOrId,
        [SwaggerParameter(Description = "The language of Pokemon, fx: 'en'", Required = false)] string? language)
    {
        var result= await _pokemonManager.GetPokemonInformationAsync(nameOrId, language);
        return Ok(result);
    }
}
