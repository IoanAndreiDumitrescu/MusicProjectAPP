using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace MusicProjectAppTests.Controllers
{
    [TestClass]
    public class CancionesControllerTests(
        Mock<IGenericRepositorio<Canciones>> mockRepoCanciones,
        Mock<IGenericRepositorio<Albumes>> mockRepoAlbumes,
        Mock<IGenericRepositorio<Artistas>> mockRepoArtistas,
        CancionesController controller)
    {
        [TestInitialize]
        public void Setup()
        {
            mockRepoCanciones = new Mock<IGenericRepositorio<Canciones>>();
            mockRepoAlbumes = new Mock<IGenericRepositorio<Albumes>>();
            mockRepoArtistas = new Mock<IGenericRepositorio<Artistas>>();
            controller = new CancionesController(mockRepoCanciones.Object, mockRepoAlbumes.Object, mockRepoArtistas.Object);
        }

        [TestMethod]
        public async Task IndexTest()
        {
            mockRepoCanciones.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Canciones, bool>>>())).ReturnsAsync(new List<Canciones>());

            var result = await controller.Index(null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            // Arrange
            var id = 1;
            mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones
            {
                Titulo = "Adele"
            });

            // Act
            var result = await controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_Get_Test()
        {
            // Arrange
            mockRepoAlbumes.Setup(repo => repo.DameTodos()).ReturnsAsync(
                new List<Albumes> { new() { Titulo = "Test Album" } });
            mockRepoArtistas.Setup(repo => repo.DameTodos()).ReturnsAsync(
                new List<Artistas> { new() { Nombre = "Test Artist" } });

            // Act
            var result = await controller.Create();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_Post_Test()
        {
            var canciones = new Canciones { Titulo = "Test Title" };
            var result = await controller.Create(canciones);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_Get_Test()
        {
            // Arrange
            var id = 1;
            mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones { Titulo = "Adele" });
            mockRepoAlbumes.Setup(repo => repo.DameTodos()).ReturnsAsync(new List<Albumes> { new() { Titulo = "Test Album" } });
            mockRepoArtistas.Setup(repo => repo.DameTodos()).ReturnsAsync(new List<Artistas> { new() { Nombre = "Test Artist" } });

            // Act
            var result = await controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_Post_Test()
        {
            // Arrange
            var id = 1;
            mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones { Id = id, Titulo = "Cancion Test" });

            Canciones cancion = new Canciones { Id = id, Titulo = "Cancion Test" };

            // Act
            var result = await controller.Edit(id, cancion);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public async Task Delete_Confirmed_Test()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await controller.DeleteConfirmed(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public async Task Delete_Test()
        {
            // Arrange
            var id = 1;
            mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones
            {

                Titulo = "Adele"
            });

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}