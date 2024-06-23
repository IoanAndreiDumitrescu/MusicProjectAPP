using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.ControllersTest
{
    [TestClass]
    public class AlbumesControllerTests
    {
        private Mock<IGenericRepositorio<Albumes>> _mockRepo = null!;
        private AlbumesController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IGenericRepositorio<Albumes>>();
            _controller = new AlbumesController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfAlbumes()
        {
            _mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Albumes, bool>>>())).ReturnsAsync([
                new Albumes { Id = 1, Genero = "Genre1", Fecha = DateTime.Now, Titulo = "Title1" },
                new Albumes { Id = 2, Genero = "Genre2", Fecha = DateTime.Now, Titulo = "Title2" }
            ]);

            var result = await _controller.Index(null);
            var viewResult = result as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model as IEnumerable<Albumes>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public async Task Details_ReturnsNotFound_ForNullId()
        {
            var result = await _controller.Details(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ReturnsNotFound_ForInvalidId()
        {
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((Albumes)null!);

            var result = await _controller.Details(999);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ReturnsView_WithAlbumes()
        {
            var expectedAlbum = new Albumes { Id = 1, Genero = "Genre1", Fecha = DateTime.Now, Titulo = "Title1" };
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(expectedAlbum);

            var result = await _controller.Details(1);

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model as Albumes;
            Assert.IsNotNull(model);
            Assert.AreEqual(expectedAlbum, model);
        }

        [TestMethod]
        public async Task CreatePost_ReturnsRedirectToActionResult_RedirectsToIndex()
        {
            var newAlbum = new Albumes { Genero = "NewGenre", Fecha = DateTime.Now, Titulo = "NewTitle" };

            var result = await _controller.Create(newAlbum);

            Assert.IsNotNull(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
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

        [TestMethod]
        public async Task Edit_ReturnsNotFoundResult_WhenIdIsNull()
        {
            var result = await _controller.Edit(id: null);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_ReturnsNotFoundResult_WhenIDoesNotExist()
        {
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((Albumes)null!);

            var result = await _controller.Edit(id: 1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_ReturnsViewResult_WithAlbumes()
        {
            var expectedAlbum = new Albumes { Id = 1, Genero = "Genre1", Fecha = DateTime.Now, Titulo = "Title1" };
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(expectedAlbum);

            var result = await _controller.Edit(id: 1);

            var viewResult = result as ViewResult;
            var model = viewResult?.ViewData.Model as Albumes;

            Assert.IsNotNull(model);
            Assert.AreEqual(expectedAlbum, model);
        }

    }
}