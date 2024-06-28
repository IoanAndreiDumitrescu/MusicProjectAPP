using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.Controllers
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
            // Arrange
            var id = 1;
            _mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones
            {
                Titulo = "Adele"
            });

            // Act
            var result = await _controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [TestMethod]
        public async Task Create_Get_Test()
        {
            // Arrange
            /*
             * The goal here is to set up conditions for the test. Mock objects are prepared using Moq library, which essentially
             * are "fake" implementations of interfaces/classes that your method under test depends on. The purpose of these mocks
             * is to isolate the code you are testing.
               Here, it seems like the Create action depends on repo.DameTodos() for both Albumes and Artistas repositories.
               For Albumes repository's DameTodos method, it sets up to return a list with one Albumes object that has the Titulo of "Test Album".
               For Artistas repository's DameTodos method, it sets up to return a list with one Artistas object that has the Nombre of "Test Artist"
             */
            _mockRepoAlbumes.Setup(repo => repo.DameTodos()).ReturnsAsync(
                new List<Albumes> { new Albumes { Titulo = "Test Album" } });
            _mockRepoArtistas.Setup(repo => repo.DameTodos()).ReturnsAsync(
                new List<Artistas> { new Artistas { Nombre = "Test Artist" } });

            // Act
            /*
             * This is where the method being tested is called. The Create method on
             * the _controller object is called here. This method is expected to return a view.
             */
            var result = await _controller.Create();

            /*
             * This is where the test verifies that the expectations were met.
               Here, it checks that the action result returned from the call to _controller.Create() is an instance
            of ViewResult which implies that the action is returning a view.
             */
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_Post_Test()
        {
            var canciones = new Canciones { Titulo = "Test Title" }; 
            var result = await _controller.Create(canciones);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_Get_Test()
        {
            // Arrange
            var id = 1;
            _mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones { Titulo = "Adele" });
            _mockRepoAlbumes.Setup(repo => repo.DameTodos()).ReturnsAsync(new List<Albumes> { new Albumes { Titulo = "Test Album" } });
            _mockRepoArtistas.Setup(repo => repo.DameTodos()).ReturnsAsync(new List<Artistas> { new Artistas { Nombre = "Test Artist" } });

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_Post_Test()
        {
            // Arrange
            var id = 1;
            _mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones { Id = id, Titulo = "Cancion Test" });

            Canciones cancion = new Canciones { Id = id, Titulo = "Cancion Test" };

            // Act
            var result = await _controller.Edit(id, cancion);

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
            var result = await _controller.DeleteConfirmed(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public async Task Delete_Test()
        {
            // Arrange
            var id = 1;
            _mockRepoCanciones.Setup(repo => repo.DameUno(id)).ReturnsAsync(new Canciones
            {
                
                Titulo = "Adele"
            });

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}

/*
 * The Arrange-Act-Assert (AAA) pattern is a common way of writing unit tests for a method under test. As the name suggests, it consists of three main actions:
   1. Arrange: This involves setting up everything needed for the test. It may include creating mock objects, setting up object properties, initializing inputs, and otherwise preparing the test environment.
   For example, if we're testing a function that sorts a list, we'd create and initialize the list in the Arrange step.
   2. Act: This part of the AAA pattern involves executing the actual method or function under test with the conditions defined in the Arrange step.
   Continuing from our example, the Act step would involve calling the sort function on the list.
   3. Assert: After execution in the Act step, we then use the Assert step to verify that the function or method behaved as expected. This typically involves checking return values or changes in object states against expected results.
   For our sort function, the Assert step would verify that the list is sorted correctly.

   Here is a code snippet to demonstrate a test following the AAA pattern:

[TestMethod]
   public void TestSortingAlgorithm() {
       // Arrange
       var myList = new List<int>() {2, 1, 3};
       
       // Act
       myList.Sort();
       
       // Assert
       Assert.AreEqual(1, myList[0]); // Check that the first element is 1
       Assert.AreEqual(2, myList[1]); // Check that the second element is 2
       Assert.AreEqual(3, myList[2]); // Check that the third element is 3
   }
 */