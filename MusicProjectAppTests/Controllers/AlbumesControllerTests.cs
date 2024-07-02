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
    public class AlbumesControllerTest
    {
        private IConfiguration configuration;
        private AlbumesController miControladorAProbar;
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
            var repo = new EFGenericRepositorio<Albumes>(context);
            miControladorAProbar = new AlbumesController(repo);
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
           
            var result = await miControladorAProbar.Index(string.Empty) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var listaAlbums = result.ViewData.Model as List<Albumes>;
            Assert.IsNotNull(listaAlbums);

            
        }

        [TestMethod]
        public async Task DetailsTest()
        {
            var result = await miControladorAProbar.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);
            var album = result.ViewData.Model as Albumes;
            Assert.IsNotNull(album);
        }

        [TestMethod]
        public async Task CreateGetTest()
        {
            var result = miControladorAProbar.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreatePostTest()
        {
            Albumes newAlbum = new Albumes
            {
                Titulo = "New Album Title"
            };

            var result = await miControladorAProbar.Create(newAlbum) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            Albumes retrievedAlbum = context.Albumes.FirstOrDefault(a => a.Titulo == newAlbum.Titulo);
            Assert.IsNotNull(retrievedAlbum);
            Assert.AreEqual(newAlbum.Titulo, retrievedAlbum.Titulo);
        }

        [TestMethod]
        public async Task EditGetTest()
        {
            var testAlbumId = 1;
            var result = await miControladorAProbar.Edit(testAlbumId) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Albumes));

            var album = result.Model as Albumes;
            Assert.AreEqual(testAlbumId, album.Id);
        }

        [TestMethod]
        public async Task EditPostTest()
        {
            var testAlbumId = 1;
            var editedAlbum = new Albumes
            {
                Id = testAlbumId,
                Titulo = "Edited Album Title"
            };

            var result = await miControladorAProbar.Edit(testAlbumId, editedAlbum) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            var retrievedAlbum = await context.Albumes.FindAsync(testAlbumId);
            Assert.AreEqual(editedAlbum.Titulo, retrievedAlbum.Titulo);
        }

        [TestMethod]
        public async Task DeleteGetTest()
        {
            var testAlbumId = 1;
            var result = await miControladorAProbar.Delete(testAlbumId) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Albumes));

            var album = result.Model as Albumes;
            Assert.AreEqual(testAlbumId, album.Id);
        }
        [TestMethod]
        public async Task DeletePostTest()
        {
            var albumToBeDeleted = await context.Albumes.FindAsync(1);
            Assert.IsNotNull(albumToBeDeleted);

            context.Canciones.RemoveRange(context.Canciones.Where(c => c.AlbumId == albumToBeDeleted.Id));
            await context.SaveChangesAsync();

            await miControladorAProbar.DeleteConfirmed(albumToBeDeleted.Id);

        }

        [TestMethod]
        public async Task AlbumesPorCancionTest_ValidSongName()
        {
            string validSongName = "Walk This Way";

            var result = await miControladorAProbar.AlbumesPorCancion(validSongName) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.Model);

            var albumList = result.ViewData.Model as List<Albumes>;
            Assert.IsNotNull(albumList);

            foreach (var album in albumList)
            {
                Assert.IsTrue(album.Canciones.Any(song => song.Titulo == validSongName));
            }
        }
    }
}