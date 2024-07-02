using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.Controllers;

[TestClass]
public class CancionesControllerTest
{
    private IConfiguration configuration;
    private CancionesController miControladorAProbar;
    private IDbContextTransaction transaction;
    private GrupoAContext context;


    [TestInitialize]
    public void TestInitialize()
    {
        configuration = InitConfiguration();
        var optionsBuilder = new DbContextOptionsBuilder<GrupoAContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDatabase"));
        context = new GrupoAContext(optionsBuilder.Options);
        transaction = context.Database.BeginTransaction();
        var cancionesRepositorio = new EFGenericRepositorio<Canciones>(context);
        var albumesRepositorio = new EFGenericRepositorio<Albumes>(context);
        var artistasRepositorio = new EFGenericRepositorio<Artistas>(context);
        miControladorAProbar = new CancionesController(cancionesRepositorio, albumesRepositorio, artistasRepositorio);
    }
    [TestCleanup]
    public void TestCleanup()
    {
        transaction.Rollback(); 
        context.Dispose(); 
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
        var result = await miControladorAProbar.Index("") as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);
        var listaCanciones = result.ViewData.Model as List<Canciones>;
        Assert.IsNotNull(listaCanciones);
    }

    [TestMethod]
    public async Task IndexTest_SearchString()
    {
        var result = await miControladorAProbar.Index("Walk This Way") as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);
        var listaCanciones = result.ViewData.Model as List<Canciones>;
        //Assert.AreEqual("Walk This Way", listaCanciones[2].Titulo);
        Assert.IsTrue(listaCanciones.Any(x => x.Titulo == "Walk This Way"));
    }

    [TestMethod]
    public async Task DetailsTest()
    {
        var id = 3; 
        var result = await miControladorAProbar.Details(id) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var cancion = result.ViewData.Model as Canciones;
        Assert.IsNotNull(cancion);
    }

    [TestMethod]
    public async Task DetailsTest_Null_ID()
    {
        var result = await miControladorAProbar.Details(null);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task EditTest_Invalid_ID()
    {
        var invalidId = 999;
        var result = await miControladorAProbar.Edit(invalidId);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
    

    [TestMethod]
    public async Task Create_Get_Test()
    {
        var result = await miControladorAProbar.Create() as ViewResult;
        Assert.IsNotNull(result);
    }


    [TestMethod]
    public async Task Create_Post_Test()
    {
        var newCancion = new Canciones
        {
            Titulo = "Test Titulo",
        };
        var result = await miControladorAProbar.Create(newCancion) as RedirectToActionResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
    }

    [TestMethod]
    public async Task Create_Post_Test_Invalid_ModelState()
    {
        miControladorAProbar.ModelState.AddModelError("ModelError", "Any Error Here");
        var newCancion = new Canciones { Titulo = "Test Title" };
        var result = await miControladorAProbar.Create(newCancion) as ViewResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancionesExists_Test()
    {
        var id = 21; 
        var exists = await miControladorAProbar.CancionesExists(id);
        Assert.IsTrue(exists);
    }

    [TestMethod]
    public async Task EditGetTest()
    {
        var id = 21; 
        var result = await miControladorAProbar.Edit(id) as ViewResult;
        Assert.IsNotNull(result);
        var cancion = result.ViewData.Model as Canciones;
        Assert.IsNotNull(cancion);
    }

    [TestMethod]
    public async Task EditPostTest()
    {
        var id = 21;
        var cancion = await context.Canciones.FindAsync(id);
        cancion.Titulo = "New Title"; 
        var result = await miControladorAProbar.Edit(id, cancion) as RedirectToActionResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
    }

    [TestMethod]
    public async Task EditPostWithIdNotMatchingCancionesId()
    {
        var invalidId = 999;
        var existingCancion = new Canciones { Titulo = "Existing Titulo", Id = 1 };
        var result = await miControladorAProbar.Edit(invalidId, existingCancion);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task EditPostTestInvalidID()
    {
        var invalidId = 999;
        var canciones = new Canciones { Titulo = "Test Title", Id = 1 };
        var result = await miControladorAProbar.Edit(invalidId, canciones) as NotFoundResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task EditPostTestInvalidModelState()
    {
        miControladorAProbar.ModelState.AddModelError("ModelError", "Any Error Here");
        var existingCancion = new Canciones { Titulo = "Existing Titulo", Id = 1 };
        var result = await miControladorAProbar.Edit(1, existingCancion) as ViewResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task DeleteTestNullID()
    {
        var result = await miControladorAProbar.Delete(null);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteGetTest()
    {
        var id = 21; 
        var result = await miControladorAProbar.Delete(id) as ViewResult;
        Assert.IsNotNull(result);
        var cancion = result.ViewData.Model as Canciones;
        Assert.IsNotNull(cancion);
    }

    [TestMethod]
    public async Task DeletePostTest()
    {
        var idToDelete = 21; 
        var initialRecords = await context.Canciones.CountAsync();

        await miControladorAProbar.DeleteConfirmed(idToDelete);

        var recordsAfterDeletion = await context.Canciones.CountAsync();
        Assert.AreEqual(initialRecords - 1, recordsAfterDeletion);
    }


}