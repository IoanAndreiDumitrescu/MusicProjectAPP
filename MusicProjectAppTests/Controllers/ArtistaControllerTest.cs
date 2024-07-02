using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction transaction;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            return config;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            configuration = InitConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<GrupoAContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDatabase"));
            context = new GrupoAContext(optionsBuilder.Options);
            transaction = context.Database.BeginTransaction();
            var repo = new EFGenericRepositorio<Artistas>(context);
            miControladorAProbar = new ArtistasController(repo);   
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            context.Dispose();
        }

        [TestMethod]
        public async Task IndexTest()
        {
            var result = await miControladorAProbar.Index("") as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            System.Diagnostics.Debug.WriteLine(result.ViewData.Model.GetType());
            var listaArtistas = result.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);

            Assert.AreEqual(4, listaArtistas.Count);
        }

        [TestMethod]
        public async Task CancionesPorArtistaTest()
        {
            var searchString = "David Bowie"; 
            var result = await miControladorAProbar.CancionesPorArtista(searchString) as ViewResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(result.ViewData.Model, "Model is null");

            var artistas = result.ViewData.Model as IEnumerable<Artistas>;
            Assert.IsNotNull(artistas, "Model is not of type IEnumerable<Artistas>");
            Assert.IsTrue(artistas.Any(a => a.Nombre == searchString), $"Expected artist '{searchString}' not found in the model");
        }



        [TestMethod]
        public async Task DetailsTest()
        {
            var result = await miControladorAProbar.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);

            var artista = result.ViewData.Model as Artistas;
            Assert.IsNotNull(artista);
            Assert.AreEqual("David Bowie", artista.Nombre); 
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var result = await miControladorAProbar.Delete(1) as ViewResult;
            Assert.IsNotNull(result);

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
            var result = await miControladorAProbar.DeleteConfirmed(2) as RedirectToActionResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            var indexResult = await miControladorAProbar.Index("") as ViewResult;
            Assert.IsNotNull(indexResult);
            Assert.IsNotNull(indexResult.ViewData.Model);

            var listaArtistas = indexResult.ViewData.Model as List<Artistas>;
            Assert.IsNotNull(listaArtistas);
            Assert.AreEqual(4, listaArtistas.Count);
        }


        [TestMethod]
        public async Task EditPostTest()
        {
            int testArtistId = 1;

            Artistas editedArtist = new Artistas
            {
                Id = testArtistId,
                Nombre = "New Artist Name",
                Genero = "New Genre",
                Fecha = DateTime.Now
            };

            var result = await miControladorAProbar.Edit(testArtistId, editedArtist) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            Artistas retrievedArtist = await context.Artistas.FindAsync(testArtistId);
            Assert.AreEqual(editedArtist.Nombre, retrievedArtist.Nombre);
            Assert.AreEqual(editedArtist.Genero, retrievedArtist.Genero);
            Assert.AreEqual(editedArtist.Fecha, retrievedArtist.Fecha);
        }

        [TestMethod]
        public async Task EditGetTest()
        {
            int testArtistId = 1;
            var result = await miControladorAProbar.Edit(testArtistId) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Artistas));
            Artistas artist = result.Model as Artistas;
            Assert.AreEqual(testArtistId, artist.Id);
        }
    }
}
