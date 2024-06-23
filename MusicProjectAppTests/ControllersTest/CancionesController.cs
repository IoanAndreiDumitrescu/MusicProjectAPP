using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.ControllersTest
{
    [TestClass]
    public class CancionesControllerTests
    {
        private Mock<IGenericRepositorio<Canciones>> _cancionesRepo = null!;
        private Mock<IGenericRepositorio<Artistas>> _artistasRepo = null!;
        private Mock<IGenericRepositorio<Albumes>> _albumesRepo = null!;
        private CancionesController _controller = null!;

        public CancionesControllerTests() // Parameter-less Constructor
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            _cancionesRepo = new Mock<IGenericRepositorio<Canciones>>();
            _artistasRepo = new Mock<IGenericRepositorio<Artistas>>();
            _albumesRepo = new Mock<IGenericRepositorio<Albumes>>();
            _controller = new CancionesController(_cancionesRepo.Object, _albumesRepo.Object, _artistasRepo.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult_WithListOfCanciones()
        {
            _cancionesRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Canciones, bool>>>())).ReturnsAsync(new List<Canciones> {
                new Canciones { Id = 1, Titulo = "Song1" },
                new Canciones { Id = 2, Titulo = "Song2"}
            });

            var result = await _controller.Index(null);

            var viewResult = result as ViewResult;
            var model = viewResult?.ViewData.Model as List<Canciones>;
            Assert.AreEqual(2, model!.Count);
        }

        [TestMethod]
        public async Task Details_ReturnsView_WithCanciones()
        {
            var canciones = new Canciones { Id = 1, Titulo = "Song1" };
            _cancionesRepo.Setup(repo => repo.DameUno(It.IsAny<int>())).ReturnsAsync(canciones);

            var result = await _controller.Details(1);

            var viewResult = result as ViewResult;
            var model = viewResult!.ViewData.Model as Canciones;
            Assert.AreEqual(canciones, model);
        }

        [TestMethod]
        public async Task CreatePost_RedirectsToIndex()
        {
            var nuevasCanciones = new Canciones { Id = 3, Titulo = "Song3" };
            var result = await _controller.Create(nuevasCanciones);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public async Task EditPost_RedirectsToIndex()
        {
            var cancionesToEdit = new Canciones { Id = 1, Titulo = "Song1" };
            var result = await _controller.Edit(cancionesToEdit.Id, cancionesToEdit);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public async Task EditPost_ReturnsView_WhenModelStateInvalid()
        {
            _controller.ModelState.AddModelError("test", "error");
            var canciones = new Canciones { Id = 3, Titulo = "Song3" };

            var result = await _controller.Edit(3, canciones);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = ((ViewResult)result).Model;
            Assert.AreEqual(canciones, model);
        }

        [TestMethod]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            var result = await _controller.DeleteConfirmed(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }
    }
}