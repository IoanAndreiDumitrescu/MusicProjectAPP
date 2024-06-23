using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.ControllersTest
{
    [TestClass]
    public class ArtistasControllerTests
    {
        private Mock<IGenericRepositorio<Artistas>> _mockRepo = null!;
        private ArtistasController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IGenericRepositorio<Artistas>>();
            _controller = new ArtistasController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfArtistas()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Artistas, bool>>>())).ReturnsAsync(new List<Artistas> {
                new Artistas { Id = 1, Nombre = "Artist1", Genero = "Genre1", Fecha = DateTime.Now },
                new Artistas { Id = 2, Nombre = "Artist2", Genero = "Genre2", Fecha = DateTime.Now }
            });

            // Act
            var result = await _controller.Index(null!);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model as IEnumerable<Artistas>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        // ... Your other test methods ...

        [TestMethod]
        public async Task CreatePost_ReturnsRedirectToActionResult_RedirectsToIndex()
        {
            var newArtista = new Artistas { Nombre = "NewArtist", Genero = "NewGenre", Fecha = DateTime.Now };

            var result = await _controller.Create(newArtista);

            Assert.IsNotNull(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound_ForNullId()
        {
            var result = await _controller.Delete(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsCorrectView_ForValidId()
        {
            var expectedArtist = new Artistas { Id = 1, Nombre = "Artist1", Genero = "Genre1", Fecha = DateTime.Now };
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(expectedArtist);

            var result = await _controller.Delete(1);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model as Artistas;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedArtist, model);
        }

        [TestMethod]
        public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_ReturnsNotFoundResult_WhenIDoesNotExist()
        {
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((Artistas)null!);

            var result = await _controller.Edit(id: 1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_ReturnsViewResult_WithArtistas()
        {
            var expectedArtist = new Artistas { Id = 1, Nombre = "Artist1", Genero = "Genre1", Fecha = DateTime.Now };
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(expectedArtist);

            var result = await _controller.Edit(id: 1);

            var viewResult = result as ViewResult;
            var model = viewResult?.ViewData.Model as Artistas;

            Assert.IsNotNull(model);
            Assert.AreEqual(expectedArtist, model);
        }

        [TestMethod]
        public async Task DeleteConfirmed_ReturnsRedirectToActionResult_RedirectsToIndex()
        {
            var deleteId = 1;
            var result = await _controller.DeleteConfirmed(deleteId);

            Assert.IsNotNull(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
    }
}