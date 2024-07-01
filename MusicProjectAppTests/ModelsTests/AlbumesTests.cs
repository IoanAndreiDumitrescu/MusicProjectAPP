using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class AlbumesTests
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
                Titulo = "Test Album",
                Canciones = new List<Canciones>
                {
                    new Canciones { Titulo = "Song 1" },
                    new Canciones { Titulo = "Song 2" }
                    // add more songs if needed
                }
            };

            // Act
            var canciones = album.Canciones;
            var id = album.Id;
            var genero = album.Genero;
            var fecha = album.Fecha;
            var titulo = album.Titulo;
            var canciones = album.Canciones;

            // Assert
          
            Assert.AreEqual(10, id);
            Assert.AreEqual("Rock", genero);
            Assert.AreEqual("Test Album", titulo);
            Assert.IsTrue(DateTime.Now >= fecha);
            Assert.IsNotNull(canciones);
            Assert.IsInstanceOfType(canciones, typeof(List<Canciones>));
            Assert.IsTrue(canciones.Count > 0); // check if list has items 
        }
    }
}