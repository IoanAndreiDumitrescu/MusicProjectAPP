
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectApp.Controllers.Tests
{
    [TestClass()]
    public class  AlbumesControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        private AlbumesController miControladoAProbar = new AlbumesController(
           
            new EFGenericRepositorio<Albumes>(InitConfiguration()));
            

        [TestMethod()]
        public void IndexTest()
        {
            var result = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaAlbumes = result.ViewData.Model as List<Albumes>;
            Assert.IsNotNull(listaAlbumes);
            Assert.AreEqual(5, listaAlbumes.Count);
            var resultado = miControladoAProbar.Index("").Result as ViewResult;
        }

        [TestMethod()]
        public void AlbumesPorCancionTest()
        {
            
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = miControladoAProbar.Details(4).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaAlbumes = result.ViewData.Model as Albumes;
            Assert.IsNotNull(listaAlbumes);
            Assert.AreEqual("Lista de albumes", listaAlbumes.Titulo);

        }

        [TestMethod()]
        public void CreateTest()
        {
            

        }

        [TestMethod()]
        public void CreateTest1()
        {
            
        }

        [TestMethod()]
        public void EditTest()
        {
            var result = miControladoAProbar.Edit(2).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaAlbumes= result.ViewData.Model as Albumes;
            Assert.IsNotNull(listaAlbumes);
            Assert.AreEqual("Lista de albumes", listaAlbumes.Titulo);

        }

        [TestMethod()]
        public void EditTest1()
        {
            var result = miControladoAProbar.Edit(2).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaAlbumes = result.ViewData.Model as Albumes;
            Assert.IsNotNull(listaAlbumes);
            Assert.AreEqual("Lista de albumes", listaAlbumes.Titulo);

        }

        [TestMethod()]
        public void DeleteTest()
        {
            var result = miControladoAProbar.Delete(1).Result as ViewResult;
            result = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaAlbumes = result.ViewData.Model as List<Albumes>;
            Assert.IsNotNull(listaAlbumes);
            Assert.AreEqual(4, listaAlbumes.Count);
            var resultado = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            var result = miControladoAProbar.DeleteConfirmed(1).Result as ViewResult;
            
            Assert.IsNull(result);
            
            
        }
    }
}
