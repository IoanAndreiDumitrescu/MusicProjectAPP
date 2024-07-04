using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectApp.Services.Specifications.Canciones;
using MusicProjectApp.Models;

namespace MusicProjectApp.Tests
{
    [TestClass]
    public class AlbumSpecificationTests
    {
        [TestMethod]
        public void IsValid_ReturnsTrue_WhenAlbumIdIsMatched()
        {
            // Arrange
            int testAlbumId = 1;
            var spec = new AlbumSpecification(testAlbumId);
            var cancion = new Canciones { AlbumId = testAlbumId, Titulo = "Test Title" };

            // Act
            var result = spec.IsValid(cancion);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_ReturnsFalse_WhenAlbumIdIsNotMatched()
        {
            // Arrange
            int testAlbumId = 1;
            var spec = new AlbumSpecification(testAlbumId);
            var cancion = new Canciones { AlbumId = testAlbumId + 1, Titulo = "Test Title" };

            // Act
            var result = spec.IsValid(cancion);

            // Assert
            Assert.IsFalse(result);
        }
    }
}