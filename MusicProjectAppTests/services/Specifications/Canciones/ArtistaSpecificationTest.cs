using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectApp.Services.Specifications.Canciones;
using MusicProjectApp.Models;

namespace MusicProjectApp.Tests
{
    [TestClass]
    public class ArtistaSpecificationTests
    {
        [TestMethod]
        public void IsValid_ReturnsTrue_WhenArtistaIdIsMatched()
        {
            // Arrange
            int testArtistaId = 1;
            var spec = new ArtistaSpecification(testArtistaId);
            var cancion = new Canciones { ArtistaId = testArtistaId, Titulo = "Test Title" };

            // Act
            var result = spec.IsValid(cancion);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_ReturnsFalse_WhenArtistaIdIsNotMatched()
        {
            // Arrange
            int testArtistaId = 1;
            var spec = new ArtistaSpecification(testArtistaId);
            var cancion = new Canciones { ArtistaId = testArtistaId + 1, Titulo = "Test Title" };

            // Act
            var result = spec.IsValid(cancion);

            // Assert
            Assert.IsFalse(result);
        }
    }
}