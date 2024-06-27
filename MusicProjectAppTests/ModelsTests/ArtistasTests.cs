using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class ArtistasTests
    {
        [TestMethod]
        public void Test_Artistas_Creation()
        {
            // Arrange
            var artist = new Artistas
            {
                Id = 1,
                Nombre = "Test Artist",
                Genero = "Rock",
                Fecha = DateTime.Now,
                Canciones = new List<Canciones>(),
                Festival = new List<Festival>(),
            };

            // Act
            var id = artist.Id;
            var nombre = artist.Nombre;
            var genero = artist.Genero;
            var fecha = artist.Fecha;
            var canciones = artist.Canciones;
            var festival = artist.Festival;

            // Assert
            Assert.AreEqual(1, id);
            Assert.AreEqual("Test Artist", nombre);
            Assert.AreEqual("Rock", genero);
            Assert.IsTrue(DateTime.Now >= fecha); 
            Assert.IsNotNull(canciones);
            Assert.IsNotNull(festival);
        }
    }
}