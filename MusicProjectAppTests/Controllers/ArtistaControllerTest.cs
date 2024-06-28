﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicProjectApp.Controllers.Tests
{
    [TestClass()]
    public class ArtistaControllerTest
    {


        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        private ArtistasController miControladoAProbar = new ArtistasController(
          
            new EFGenericRepositorio<Artistas>(InitConfiguration()));

        [TestMethod()]
        public void IndexTest()
        {
            var result = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaArtistas = result.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);
            Assert.AreEqual(5, listaArtistas.Count);
            var resultado = miControladoAProbar.Index("").Result as ViewResult;

        }

        [TestMethod()]
        public void CancionesPorArtistaTest()
        {

        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = miControladoAProbar.Details(2).Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var listaArtistas = result.ViewData.Model as Artistas;
            Assert.IsNotNull(listaArtistas);
            Assert.AreEqual("Lista de artistas", listaArtistas.Nombre);

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
            var result = miControladoAProbar.Delete(2).Result as ViewResult;
            result = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaArtistas = result.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);
            Assert.AreEqual(4, listaArtistas.Count);
            var resultado = miControladoAProbar.Index("").Result as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {

        }
    }
}