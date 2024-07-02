using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

[TestClass]
public class CancionesControllerTest
{
    private IConfiguration configuration;
    private CancionesController miControladorAProbar;
    private GrupoAContext context;

    public CancionesControllerTest()
    {
        // Initialize configuration from appsettings.json
        configuration = InitConfiguration();

        // Initialize database context
        var optionsBuilder = new DbContextOptionsBuilder<GrupoAContext>();
        optionsBuilder.UseInMemoryDatabase("CancionesControllerTest");
        context = new GrupoAContext(optionsBuilder.Options);

        // Initialize repository and controller
        var cancionesRepo = new EFGenericRepositorio<Canciones>(context);
        var albumesRepo = new EFGenericRepositorio<Albumes>(context);
        var artistasRepo = new EFGenericRepositorio<Artistas>(context);
        miControladorAProbar = new CancionesController(cancionesRepo, albumesRepo, artistasRepo);

        // Initialize data
        InitializeData();
    }

    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.test.json")
           .Build();
        return config;
    }

    private void InitializeData()
    {
        var canciones = new[]
        {
            new Canciones { Id = 3, Titulo = "Walk This Way", AlbumId = 1, ArtistaId = 5 },
            new Canciones { Id = 4, Titulo = "Another Brick in the Wall", AlbumId = 3, ArtistaId = 1 },
            new Canciones { Id = 12, Titulo = "Light My Fire", AlbumId = 3, ArtistaId = 1 },
        };
        context.Canciones.AddRange(canciones);
        context.SaveChanges();
    }

    [TestMethod]
    public async Task IndexTest()
    {
        // Test Index action
        var result = await miControladorAProbar.Index("") as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);
        var listaCanciones = result.ViewData.Model as List<Canciones>;
        Assert.IsNotNull(listaCanciones);
    }

    [TestMethod]
    public async Task DetailsTest()
    {
        var id = 3; // Ensure ID 3 exists in your test data
        var result = await miControladorAProbar.Details(id) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var cancion = result.ViewData.Model as Canciones;
        Assert.IsNotNull(cancion);
        Assert.AreEqual("Walk This Way", cancion.Titulo); 
    }

    [TestMethod]
    public async Task Create_Get_Test()
    {
        // Act
        var result = await miControladorAProbar.Create();

        // Assert
        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public async Task Create_Post_Test()
    {
        var cancion = new Canciones { Titulo = "New Song" };
        var result = await miControladorAProbar.Create(cancion);
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
    }

    [TestMethod]
    public async Task Edit_Get_Test()
    {
        var id = 4; // Ensure ID 4 exists in your test data
        var result = await miControladorAProbar.Edit(id) as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ViewData.Model);

        var cancion = result.ViewData.Model as Canciones;
        Assert.IsNotNull(cancion);
        Assert.AreEqual("Updated Song", cancion.Titulo); // Ensure the song's title matches the ID 4
    }

    [TestMethod]
    public async Task Edit_Post_Test()
    {
        var id = 4; // Ensure ID 4 exists in your test data
        var cancion = new Canciones { Id = id, Titulo = "Updated Song" };
        var result = await miControladorAProbar.Edit(id, cancion);
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }

    [TestMethod]
    public async Task Delete_Confirmed_Test()
    {
        var id = 5; // Ensure ID 5 exists in your test data
        var result = await miControladorAProbar.DeleteConfirmed(id);
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);
    }

    [TestMethod]
    public async Task Delete_Test()
    {
        var id = 5; // Ensure ID 5 exists in your test data
        var result = await miControladorAProbar.Delete(id) as ViewResult;
        Assert.IsNotNull(result.ViewData.Model);

        var cancion = result.ViewData.Model as Canciones;
        Assert.IsNotNull(cancion);
        Assert.AreEqual("Livin' on a Prayer", cancion.Titulo); // Ensure the song's title matches the ID 5
    }
}