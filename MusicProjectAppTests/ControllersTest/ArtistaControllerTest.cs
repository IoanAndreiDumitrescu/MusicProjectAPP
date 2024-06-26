
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectAppTests.ControllersTest
{
    [TestClass]
    public class ArtistasControllerTest(
        Mock<IGenericRepositorio<Artistas>> mockRepo,
        ArtistasController controller,
        Artistas artista)
    {
        [TestInitialize]
        public void TestInitialize()
        {
            mockRepo = new Mock<IGenericRepositorio<Artistas>>();
            controller = new ArtistasController(mockRepo.Object);
            artista = new() { Id = 1, Nombre = "Artista 1", Genero = "Rock", Fecha = DateTime.Now };
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfArtistas()
        {
            // Arrange
            var mockArtistas = new List<Artistas> { artista };
            mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Artistas, bool>>>()))
                .ReturnsAsync(mockArtistas);

            // Act
            var result = await controller.Index(null!);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<Artistas>));
            var model = viewResult.ViewData.Model as IEnumerable<Artistas>;
            Assert.AreEqual(1, model?.Count());
        }

        [TestMethod]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ReturnsNotFound_WhenArtistaDoesNotExist()
        {
            // Arrange
            mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync((Artistas)null!);

            // Act
            var result = await controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_ReturnsViewResult_WithArtista()
        {
            // Arrange
            mockRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(artista);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.ViewData.Model as Artistas;
            Assert.AreEqual(artista, model);
        }

        [TestMethod]
        public async Task Create_Post_ReturnsRedirectToAction_WhenModelStateIsValid()
        {
            // Arrange
mockRepo.Setup(repo => repo.Agregar(artista)).Returns(Task.FromResult(true));            // Act
            var result = await controller.Create(artista);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult?.ActionName);
        }
        [TestMethod]
        public async Task Edit_Post_ReturnsNotFound_WhenIdIsDifferent()
        {
            // Act
            var result = await controller.Edit(2, artista); // Note that artista id is 1 and we are passing 2 here

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_Post_ReturnsRedirectToAction_WhenModelStateIsValid()
        {
            // Arrange
            mockRepo.Setup(repo => repo.Modificar(artista.Id, artista)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.Edit(1, artista);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public async Task DeleteConfirmed_Post_ReturnsRedirectToAction()
        {
            // Arrange
            mockRepo.Setup(repo => repo.Borrar(artista.Id)).Returns(Task.FromResult(true));
            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
    }
}