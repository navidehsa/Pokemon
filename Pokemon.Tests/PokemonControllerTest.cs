using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pokemon.Controllers;
using Pokemon.Managers;
using Pokemon.Models;

namespace Pokemon.Tests
{
    public class PokemonControllerTest
    {
        private readonly Mock<IPokemonManager> _manager;
        private readonly Mock<ILogger<PokemonController>> _mockLogger;

        public PokemonControllerTest()
        {
            
            _manager = new Mock<IPokemonManager>();
            _mockLogger = new Mock<ILogger<PokemonController>>();

        }

        [Fact]
        public async Task GetPokemonAsync_ReturnsOkObjectResult()
        {
            // Arrange
            var nameOrId = "Pikachu";
            var language = "en";
            var expectedResult = new PokemonOutPutModel() { FlavorText = "test", Language = language, Version = "3", ShakespeareanDescription = "translate" };
            _manager.Setup(pm => pm.GetPokemonInformationAsync(nameOrId, language))
                .Returns(Task.FromResult(expectedResult));
            var controller = new PokemonController(_mockLogger.Object, _manager.Object);

            // Act
            var result = await controller.GetPokemonAsync(nameOrId, language);

            // Assert
            _manager.Verify(x => x.GetPokemonInformationAsync(nameOrId, language), Times.Once);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okObjectResult.Value);
        }
    }
}