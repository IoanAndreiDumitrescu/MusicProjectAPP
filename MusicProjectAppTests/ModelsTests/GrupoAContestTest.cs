using Microsoft.Extensions.Configuration;
using MusicProjectApp.Models;

namespace MusicProjectAppTests.ModelsTests
{
    [TestClass]
    public class GrupoAContextTests
    {
        [TestMethod]
        public void Test_Artistas_Can_Be_Added()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"MyDatabase", "Test_Artistas_Can_Be_Added"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            var artist = new Artistas
            {
                Id = 1,
                Nombre = "Test Artist"
            };

            // Act
            using (var context = new GrupoAContext(configuration))
            {
                context.Artistas.Add(artist);
                context.SaveChanges();
            }

            // Assert
            using (var context = new GrupoAContext(configuration))
            {
                var foundArtist = context.Artistas.FirstOrDefault(a => a.Id == artist.Id);
                Assert.IsNotNull(foundArtist);
                Assert.AreEqual(artist.Id, foundArtist.Id);
                Assert.AreEqual(artist.Nombre, foundArtist.Nombre);
            }
        }
    }
}