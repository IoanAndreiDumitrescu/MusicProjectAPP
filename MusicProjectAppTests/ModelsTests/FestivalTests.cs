using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class FestivalTests
    {
        [TestMethod]
        public void Test_Festival_Creation()
        {
            
            // Arrange
            var festival = new Festival
            {
                Artista = new Artistas { Nombre = "ACDC" },
                
                Id = 1,
                Nombre = "Test Festival",
                ArtistaId = 2,
                Ciudad = "Test City",
                FechaInicio = DateOnly.FromDateTime(DateTime.Today),
                FechaFinal = DateOnly.FromDateTime(DateTime.Today.AddDays(3))
            };

            // Act
            var artista = festival.Artista.Nombre;
            var id = festival.Id;
            var nombre = festival.Nombre;
            var artistaId = festival.ArtistaId;
            var ciudad = festival.Ciudad;
            var fechaInicio = festival.FechaInicio;
            var fechaFinal = festival.FechaFinal;

            // Assert
            Assert.AreEqual(1, id);
            Assert.AreEqual("Metalica", artista);
            Assert.AreEqual("Test Festival", nombre);
            Assert.AreEqual(2, artistaId);
            Assert.AreEqual("Test City", ciudad);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Today), fechaInicio);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Today.AddDays(3)), fechaFinal);
            Assert.AreEqual("ACDC", artista);
        }
    }
}