using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class CancionesTests
    {
        [TestMethod]
        public void Test_Canciones_Creation()
        {
            // Arrange
            var cancion = new Canciones
            {
                Id = 1,
                Titulo = "Test Song",
                ArtistaId = 2,
                AlbumId = 3
            };

            // Act
            var id = cancion.Id;
            var titulo = cancion.Titulo;
            var artistaId = cancion.ArtistaId;
            var albumId = cancion.AlbumId;

            // Assert
            Assert.AreEqual(1, id);
            Assert.AreEqual("Test Song", titulo);
            Assert.AreEqual(2, artistaId);
            Assert.AreEqual(3, albumId);
        }
    }
}