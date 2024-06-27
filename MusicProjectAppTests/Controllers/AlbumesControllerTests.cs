//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using MusicProjectApp.Models;
//using MusicProjectApp.Services.Repositorio;

//namespace MusicProjectApp.Controllers.Tests
//{
//    [TestClass()]
//    public class AlbumesControllerTests
//    {
//        [TestMethod()]
//        public void IndexTest()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void AlbumesPorCancionTest()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void DetailsTest()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void CreateTest()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void CreateTest1()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void EditTest()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void EditTest1()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void DeleteTest()
//        {
//            Assert.Fail();
//        }

//        [TestMethod()]
//        public void DeleteConfirmedTest()
//        {
//            Assert.Fail();
//        }
//    }
//}


using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectApp.Controllers.Tests
{
    [TestClass()]
    public class AlbumesControllerTests
    {
        // This is the Mock repository
        private Mock<IGenericRepositorio<Albumes>> mockRepo;
        private AlbumesController controller;

        // This method is run before each test
        [TestInitialize]
        public void Setup()
        {
            // Instantiate our Mock object
            this.mockRepo = new Mock<IGenericRepositorio<Albumes>>();

            // Setup our Mock to return a List of Albumes when GetAlbumsBySearchString() is called
            this.mockRepo.Setup(repo => repo.Filtra(It.IsAny<Expression<Func<Albumes, bool>>>()))
                            .Returns(Task.FromResult(GetSampleAlbums()));
            this.controller = new AlbumesController(mockRepo.Object);
        }

        // This is just a helper method to create a sample list of Albumes objects.
        private List<Albumes> GetSampleAlbums()
        {
            return new List<Albumes>
            {
                new Albumes { Id = 1, Genero = "Genre1", Fecha = DateTime.Now, Titulo = "Title1" },
                new Albumes { Id = 2, Genero = "Genre2", Fecha = DateTime.Now, Titulo = "Title2" }
            };
        }

        [TestMethod]
        public async Task IndexTest()
        {
            // Arrange - nothing to do because setup is done in Setup()

            // Act
            IActionResult result = await controller.Index(null);

            // Assert
            // Here we are checking that the result is a ViewResult, and that the Model returned by the ViewController is a List<Albumes> of the expected length.
            ViewResult viewResult = result as ViewResult;
            List<Albumes> model = viewResult.Model as List<Albumes>;

            Assert.IsNotNull(viewResult);
            Assert.AreEqual(2, model.Count);
        }

        // Similar test methods for other operations...

    }
}