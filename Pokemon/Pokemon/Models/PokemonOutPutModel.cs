using Swashbuckle.AspNetCore.Annotations;

namespace Pokemon.Models
{
    public class PokemonOutPutModel
    {
        [SwaggerSchema("The flavor text")]
        public string FlavorText { get; set; }

        [SwaggerSchema("The flavor text language")]
        public string Language { get; set; }

        [SwaggerSchema("The version of text")]
        public string Version { get; set; }

        [SwaggerSchema("The translated flavor text")]
        public string ShakespeareanDescription { get; set; }
    }
}
