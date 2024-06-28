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
                AlbumId = 3,
                Artista = new Artistas { Id = 2, Nombre = "Test Artist" },
                Album = new Albumes { Id = 3, Titulo = "Test Album" }
            };

            // Act
            var id = cancion.Id;
            var titulo = cancion.Titulo;
            var artistaId = cancion.ArtistaId;
            var albumId = cancion.AlbumId;
            var artista = cancion.Artista;
            var album = cancion.Album;

            // Assert
            Assert.AreEqual(1, id);
            Assert.AreEqual("Test Song", titulo);
            Assert.AreEqual(2, artistaId);
            Assert.AreEqual(3, albumId);
            Assert.IsNotNull(artista);
            Assert.AreEqual(2, artista.Id);
            Assert.AreEqual("Test Artist", artista.Nombre);
            Assert.IsNotNull(album);
            Assert.AreEqual(3, album.Id);
            Assert.AreEqual("Test Album", album.Titulo);
        }
    }
}