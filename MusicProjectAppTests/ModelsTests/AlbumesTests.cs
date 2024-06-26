using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public partial class AlbumesTests
    {
        [TestMethod]
        public void TestAlbumesCreation()
        {
            // Arrange
            var album = new Albumes
            {
                Id = 10,
                Genero = "Rock",
                Fecha = DateTime.Now,
                Titulo = "Test Album"
            };

            // Act
            var id = album.Id;
            var genero = album.Genero;
            var fecha = album.Fecha;
            var titulo = album.Titulo;

            // Assert
            Assert.AreEqual(10, id);
            Assert.AreEqual("Rock", genero);
            Assert.AreEqual("Test Album", titulo);
            Assert.IsTrue(DateTime.Now >= fecha);
        }
    }
}