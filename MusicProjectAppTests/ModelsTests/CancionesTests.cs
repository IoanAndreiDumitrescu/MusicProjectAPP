using MusicProjectApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Artista = new Artistas { Nombre = "ACDC" },
                Album = new Albumes() { Titulo = "Back in Black" },
                Id = 1,
                Titulo = "Test Song",
                ArtistaId = 2,
                AlbumId = 3,
                Artista = new Artistas { Id = 2, Nombre = "Test Artist" },
                Album = new Albumes { Id = 3, Titulo = "Test Album" }
            };

            // Act
            var artista = cancion.Artista.Nombre;
            var album = cancion.Album.Titulo;
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
