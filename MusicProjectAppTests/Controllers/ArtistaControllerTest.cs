using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicProjectAppTests.Controllers
{
    [TestClass]
    public class ArtistaControllerTest
    {
        private IConfiguration configuration;
        private ArtistasController miControladorAProbar;
        private GrupoAContext context;

        public ArtistaControllerTest()
        {
            // Initialize configuration from appsettings.json
            configuration = InitConfiguration();

            // Initialize database context
            var optionsBuilder = new DbContextOptionsBuilder<GrupoAContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDatabase"));
            context = new GrupoAContext(optionsBuilder.Options);

            // Initialize repository and controller
            var repo = new EFGenericRepositorio<Artistas>(context);
            miControladorAProbar = new ArtistasController(repo);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        [TestMethod]
        public async Task IndexTest()
        {
            // Test Index action
            var result = await miControladorAProbar.Index("") as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            System.Diagnostics.Debug.WriteLine(result.ViewData.Model.GetType());
            var listaArtistas = result.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);

            // Ensure the database has at least 5 artists for this test to pass
            Assert.AreEqual(4, listaArtistas.Count);
        }

        [TestMethod]
        public async Task CancionesPorArtistaTest()
        {
            var searchString = "David Bowie"; // Replace with an existing artist name in your test data
            var result = await miControladorAProbar.CancionesPorArtista(searchString) as ViewResult;

            // Assert that the result and its model are not null
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(result.ViewData.Model, "Model is null");

            // Assert that the model is of type IEnumerable<Artistas>
            var artistas = result.ViewData.Model as IEnumerable<Artistas>;
            Assert.IsNotNull(artistas, "Model is not of type IEnumerable<Artistas>");

            // Optionally, you can assert specific conditions about the returned artistas collection
            // For example:
            Assert.IsTrue(artistas.Any(a => a.Nombre == searchString), $"Expected artist '{searchString}' not found in the model");
        }



        [TestMethod]
        public async Task DetailsTest()
        {
            var result = await miControladorAProbar.Details(1) as ViewResult; // Ensure ID 1 exists in your test data
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);

            var artista = result.ViewData.Model as Artistas;
            Assert.IsNotNull(artista);
            Assert.AreEqual("David Bowie", artista.Nombre); // Ensure the artist's name matches the ID 1
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            // Ensure the artist with ID 1 exists and can be deleted
            var result = await miControladorAProbar.Delete(1) as ViewResult;
            Assert.IsNotNull(result);

            // After deletion, validate that there are 4 artists remaining
            var indexResult = await miControladorAProbar.Index("") as ViewResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.ViewData.Model);

            var listaArtistas = indexResult.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);
            Assert.AreEqual(4, listaArtistas.Count);
        }


        [TestMethod]
        public async Task DeleteConfirmedTest()
        {
            // Ensure the artist with ID 1 can be safely deleted
            var result = await miControladorAProbar.DeleteConfirmed(2) as RedirectToActionResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            // After deletion, validate that there are 4 artists remaining
            var indexResult = await miControladorAProbar.Index("") as ViewResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.ViewData.Model);

            var listaArtistas = indexResult.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);
            Assert.AreEqual(4, listaArtistas.Count);
        }

    }
}
