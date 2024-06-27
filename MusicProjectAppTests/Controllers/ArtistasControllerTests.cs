using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectApp.Controllers.Tests
{
    [TestClass]
    public class ArtistasControllerTests
    {
        private Mock<IGenericRepositorio<Artistas>> _mockRepo;
        private ArtistasController _controller;
        private List<Artistas> _artists;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IGenericRepositorio<Artistas>>();
            _controller = new ArtistasController(_mockRepo.Object);
            _artists =
            [
                new() { Id = 1, Nombre = "Test Artist 1" },
                new() { Id = 2, Nombre = "Test Artist 2" }
            ];
        }

        [TestMethod]
        public async Task IndexTest()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Artistas, bool>>>())).ReturnsAsync(_artists);

            // Act
            var result = await _controller.Index(null);

            // Assert
            Assert.IsNotNull(result);
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as List<Artistas>;
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public void CancionesPorArtistaTest()
        {
            Assert.Inconclusive();
            // TODO: Implement test
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((int id) => _artists.Find(a => a.Id == id));

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsNotNull(result);
            var viewResult = result as ViewResult;
            var model = viewResult.Model as Artistas;
            Assert.AreEqual("Test Artist 1", model.Nombre);
        }

        [TestMethod]
        public void CreateTest()
        {
            // TODO: Implement test
            Assert.Inconclusive();
        }

        [TestMethod]
        public async Task CreatePostTest()
        {
            // Arrange
            var newArtist = new Artistas { Id = 3, Nombre = "Test Artist 3" };
            _mockRepo.Setup(repo => repo.Agregar(It.IsAny<Artistas>())).Callback((Artistas a) => _artists.Add(a));

            // Act
            await _controller.Create(newArtist);

            // Assert
            Assert.AreEqual(3, _artists.Count);
            Assert.AreEqual("Test Artist 3", _artists[2].Nombre);
        }

        [TestMethod]
        public async Task EditTest()
        {
            // Arrange
            var updatedArtist = new Artistas { Id = 1, Nombre = "Updated Artist" };
            _mockRepo.Setup(repo => repo.Modificar(It.IsAny<int>(), It.IsAny<Artistas>())).Callback((int id, Artistas a) =>
            {
                var artistIndex = _artists.FindIndex(x => x.Id == id);
                _artists[artistIndex] = a;
            });

            // Act
            await _controller.Edit(1, updatedArtist);

            // Assert
            Assert.AreEqual("Updated Artist", _artists.First().Nombre);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Borrar(It.IsAny<int>())).Callback((int id) =>
            {
                var artist = _artists.Find(a => a.Id == id);
                _artists.Remove(artist);
            });

            // Act
            await _controller.DeleteConfirmed(1);

            // Assert
            Assert.AreEqual(1, _artists.Count);
        }
    }
}