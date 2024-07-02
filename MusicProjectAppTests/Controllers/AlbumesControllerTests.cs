using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.Controllers
{
    [TestClass]
    public class AlbumesControllerTests
    {
        private Mock<IGenericRepositorio<Albumes>> _mockRepo;
        private AlbumesController _controller;
        private List<Albumes> _albums;
        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IGenericRepositorio<Albumes>>();
            _controller = new AlbumesController(_mockRepo.Object);
            _albums =
            [
                new() { Id = 1, Titulo = "Test Album 1" },
                new() { Id = 2, Titulo = "Test Album 2" }
            ];
        }
        [TestMethod]
        public async Task IndexTest()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Albumes, bool>>>()))
                .ReturnsAsync(_albums);

            // Act
            // Call the actual method you are testing here 
            var result = await _controller.Index(null);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult, "Result is not a ViewResult");
            var model = viewResult.Model as List<Albumes>;
            Assert.IsNotNull(model, "Model is null");
            Assert.AreEqual(2, model.Count);
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((int id) => _albums.Find(a => a.Id == id));
            // Act
            var result = await _controller.Details(1);
            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as Albumes;
            Assert.AreEqual("Test Album 1", model.Titulo);
        }
        [TestMethod]
        public async Task CreateTest()
        {
            // Arrange
            var newAlbum = new Albumes { Id = 3, Titulo = "Test Album 3" };
            _mockRepo.Setup(repo => repo.Agregar(It.IsAny<Albumes>())).Callback((Albumes a) => _albums.Add(a));
            // Act
            await _controller.Create(newAlbum);
            // Assert
            Assert.AreEqual(3, _albums.Count);
            Assert.AreEqual("Test Album 3", _albums[2].Titulo);
        }
        [TestMethod]

        public async Task EditTest()
        {
            // Arrange
            var updatedAlbum = new Albumes { Id = 1, Titulo = "Updated Album" };
            _mockRepo.Setup(repo => repo.Modificar(It.IsAny<int>(), It.IsAny<Albumes>())).Callback((int id, Albumes a) =>
            {
                var albumIndex = _albums.FindIndex(x => x.Id == id);
                _albums[albumIndex] = a;  // Replace the album with the updated one
            });
            // Act
            await _controller.Edit(1, updatedAlbum);
            // Assert
            Assert.AreEqual("Updated Album", _albums.First().Titulo);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Borrar(It.IsAny<int>())).Callback((int id) =>
            {
                var album = _albums.Find(a => a.Id == id);
                _albums.Remove(album);
            });
            // Act
            await _controller.DeleteConfirmed(1);
            // Assert
            Assert.AreEqual(1, _albums.Count);
        }
    }
}
