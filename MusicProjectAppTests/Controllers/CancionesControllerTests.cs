using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectApp.Controllers.Tests
{
    [TestClass()]
    public class CancionesControllerTests
    {

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        private CancionesController miControladoAProbar = new CancionesController(
            new EFGenericRepositorio<Canciones>(InitConfiguration()),
            new EFGenericRepositorio<Albumes>(InitConfiguration()),
            new EFGenericRepositorio<Artistas>(InitConfiguration()));
        [TestMethod()]
        public void IndexTest()
        {
            var result = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaCanciones = result.ViewData.Model as List<Canciones>;
            Assert.IsNotNull(listaCanciones);
            Assert.AreEqual(5, listaCanciones.Count);
            var resultado = miControladoAProbar.Index("").Result as ViewResult;
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = miControladoAProbar.Details(2).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaCanciones = result.ViewData.Model as Canciones;
            Assert.IsNotNull(listaCanciones);
            Assert.AreEqual("Lista de canciones", listaCanciones.Titulo);

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

        }

        [TestMethod()]
        public void EditTest1()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {
            var result = miControladoAProbar.Delete(5).Result as ViewResult;
            result = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaCanciones = result.ViewData.Model as List<Canciones>;
            Assert.IsNotNull(listaCanciones);
            Assert.AreEqual(4, listaCanciones.Count);
            var resultado = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {

        }
    }
}