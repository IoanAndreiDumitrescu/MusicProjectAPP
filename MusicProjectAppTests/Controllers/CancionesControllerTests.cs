using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectApp.Controllers.Tests
{
    [TestClass]
    public class CancionesControllerTests
    {
        private Mock<IGenericRepositorio<Canciones>> _mockRepoCanciones;
        private Mock<IGenericRepositorio<Albumes>> _mockRepoAlbumes;
        private Mock<IGenericRepositorio<Artistas>> _mockRepoArtistas;
        private CancionesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepoCanciones = new Mock<IGenericRepositorio<Canciones>>();
            _mockRepoAlbumes = new Mock<IGenericRepositorio<Albumes>>();
            _mockRepoArtistas = new Mock<IGenericRepositorio<Artistas>>();
            _controller = new CancionesController(_mockRepoCanciones.Object, _mockRepoAlbumes.Object, _mockRepoArtistas.Object);
        }

        [TestMethod]
        public async Task IndexTest()
        {
            _mockRepoCanciones.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Canciones, bool>>>())).ReturnsAsync(new List<Canciones>());

            var result = await _controller.Index(null);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            var result = await _controller.Details(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_Get_Test()
        {
            var result = await _controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_Post_Test()
        {
            var canciones = new Canciones { Titulo = "Test Title" }; // Add more property values if needed
            var result = await _controller.Create(canciones);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_Get_Test()
        {
            var result = await _controller.Edit(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_Post_Test()
        {
            var canciones = new Canciones { Titulo = "Test Title" }; // Add more property values if needed
            var result = await _controller.Edit(1, canciones);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Delete_Confirmed_Test()
        {
            var result = await _controller.DeleteConfirmed(1);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Delete_Test()
        {
            var result = await _controller.Delete(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}