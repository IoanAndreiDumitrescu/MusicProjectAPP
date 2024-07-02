using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;

[TestClass()]
public class FestivalsControllerTests
{
    private IConfiguration configuration;
    private FestivalsController miControladorAProbar;
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
        miControladorAProbar = new FestivalsController(context);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        transaction.Rollback();
        context.Dispose();
    }

    [TestMethod()]
    public async Task IndexTest()
    {
        var result = await miControladorAProbar.Index() as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Model);

        var festivals = result.Model as List<Festival>;
        Assert.IsNotNull(festivals);

        int expectedCount = 8;  
        Assert.AreEqual(expectedCount, festivals.Count);
    }

    [TestMethod()]
    public void FestivalExistsTest_ValidId()
    {
        var validId = 2;

        var result = miControladorAProbar.FestivalExists(validId);

        Assert.IsTrue(result);
    }

    [TestMethod()]
    public async Task DetailsTest()
    {
        var validId = 2;

        IActionResult actionResult = await miControladorAProbar.Details(validId);
        var result = actionResult as ViewResult;

        Assert.IsNotNull(result);
        var festival = result.Model as Festival;
        Assert.IsNotNull(festival);
    }

    [TestMethod()]
    public async Task DetailsTest_NullId()
    {
        var result = await miControladorAProbar.Details(null);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod()]
    public async Task DetailsTest_ValidId()
    {
        
        var validId = 2;

        var result = await miControladorAProbar.Details(validId) as ViewResult;
        Assert.IsNotNull(result);
        var festival = result.Model as Festival;
        Assert.IsNotNull(festival);
    }

    [TestMethod]
    public void CreateGetTest()
    {
        var result = miControladorAProbar.Create() as ViewResult;
        Assert.IsNotNull(result);
        Assert.IsNull(result.Model);
    }

    [TestMethod()]
    public async Task CreateTest_ValidFestival()
    {
        var newFestival = new Festival { Nombre = "Test" };

        var actionResult = await miControladorAProbar.Create(newFestival);
        Assert.IsNotNull(actionResult, "Action result is NULL");

        var result = actionResult as RedirectToActionResult;
        Assert.IsNotNull(result, "Expected RedirectToActionResult");
        Assert.AreEqual("Index", result.ActionName, "Expected redirect to Index");
    }

    [TestMethod()]
    public async Task CreateTest_NullFestival()
    {
        await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => miControladorAProbar.Create(null));
    }

    [TestMethod()]
    public async Task CreateTest_InvalidModelState()
    {
        var newFestival = new Festival { Nombre = "Test" };
        miControladorAProbar.ModelState.AddModelError("Error", "Some error");

        var actionResult = await miControladorAProbar.Create(newFestival);
        Assert.IsNotNull(actionResult, "Action result is NULL");

        var result = actionResult as ViewResult;
        Assert.IsNotNull(result, "Expected ViewResult for invalid ModelState");
    }

    [TestMethod()]
    public async Task EditTest_ValidId()
    {
        var validId = 2;

        var result = await miControladorAProbar.Edit(validId) as ViewResult;

        Assert.IsNotNull(result);
        var festival = result.Model as Festival;

        Assert.IsNotNull(festival);
        Assert.AreEqual(validId, festival.Id); 
    }

    [TestMethod()]
    public async Task EditTest_Post_ValidFestival()
    {
        var validFestival = new Festival { Id = 2, Nombre = "Test Changed" };

        var actionResult = await miControladorAProbar.Edit(validFestival.Id, validFestival) as RedirectToActionResult;

        Assert.IsNotNull(actionResult);
        Assert.AreEqual("Index", actionResult.ActionName, "Expected redirect to Index");

        var editedFestival = await context.Festival.FindAsync(validFestival.Id);
        Assert.IsNotNull(editedFestival);

        Assert.AreEqual(validFestival.Nombre, editedFestival.Nombre);
        Assert.AreEqual(validFestival.Id, editedFestival.Id);
    }

    [TestMethod()]
    public async Task DeleteTest_ValidId()
    {
        var validId = 2;
        var result = await miControladorAProbar.Delete(validId) as ViewResult;

        Assert.IsNotNull(result);
        var festival = result.Model as Festival;

        Assert.IsNotNull(festival);
        Assert.AreEqual(validId, festival.Id); 
    }

    [TestMethod()]
    public async Task DeleteConfirmedTest_ValidId()
    {
        var validId = 2;

        var result = await miControladorAProbar.DeleteConfirmed(validId);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
    }
}