using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectAppTests.ControllersTest
{
    [TestClass]
    public class AlbumesControllerTests
    {
        private Mock<IGenericRepositorio<Albumes>> _mockRepo = null!;
        private AlbumesController _controller = null!;
        private Albumes _album = null!;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = new Mock<IGenericRepositorio<Albumes>>();
            _controller = new AlbumesController(_mockRepo.Object);
            _album = new Albumes { Id = 1, Genero = "Rock", Fecha = DateTime.Now.Date, Titulo = "Album 1" };
        }
        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfAlbumes()
        {
            // Arrange
            var mockAlbumes = new List<Albumes> { _album };
            _mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Albumes, bool>>>())).ReturnsAsync(mockAlbumes);
            // Act
            var result = await _controller.Index(null);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<Albumes>));
            var model = viewResult.ViewData.Model as IEnumerable<Albumes>;
            Assert.AreEqual(1, model!.Count());
        }


        [TestMethod]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ReturnsNotFound_WhenAlbumDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((Albumes)null!);
            // Act
            var result = await _controller.Details(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task Details_ReturnsViewResult_WithAlbum()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(_album);
            // Act
            var result = await _controller.Details(1);
            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(Albumes));
            var model = viewResult.ViewData.Model as Albumes;
            Assert.AreEqual(_album, model);
        }

       [TestMethod]
public async Task Create_Post_ReturnsRedirectToAction_WhenModelStateIsValid()
{
    // Arrange
    _mockRepo.Setup(repo => repo.Agregar(_album)).Returns(Task.FromResult(true));
    // Act
    var result = await _controller.Create(_album);
    // Assert
    Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
    var redirectToActionResult = result as RedirectToActionResult;
    Assert.IsNotNull(redirectToActionResult);
    Assert.AreEqual("Index", redirectToActionResult.ActionName);
}



        [TestMethod]
        public async Task Edit_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");
            // Act
            var result = await _controller.Edit(1, _album);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model as Albumes;
            Assert.IsNotNull(model);
            Assert.AreEqual(_album, model);
        }

        [TestMethod]
        public async Task DeleteConfirmed_Post_ReturnsRedirectToAction()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Borrar(It.IsAny<int>())).Returns(Task.FromResult(true));
            // Act
            var result = await _controller.DeleteConfirmed(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

    }
}
