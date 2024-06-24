using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicProjectApp.Controllers;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectAppTests.ControllersTest;

[TestClass]
public class CancionesControllerTests
{
    private Mock<IGenericRepositorio<Canciones>> _cancionesRepo = null!;
    private CancionesController _controller = null!;
    private List<Canciones> _cancionesList = null!;

    [TestInitialize]
    public void Setup()
    {
        _cancionesRepo = new Mock<IGenericRepositorio<Canciones>>();
        _controller = new CancionesController(_cancionesRepo.Object, null!, null);
        _cancionesList = [new Canciones() { Id = 1, Titulo = "Test Song 1", AlbumId = 1, ArtistaId = 1 }];
    }

    [TestMethod]
    public async Task Index_ShouldReturnViewResult()
    {
        var result = await _controller.Index(null) as ViewResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreatePost_ShouldRedirectToIndex()
    {
        var result = await _controller.Create(_cancionesList[0]) as RedirectToActionResult;
        Assert.AreEqual("Index", result!.ActionName);
    }

    [TestMethod]
    public async Task EditPost_ShouldRedirectToIndex()
    {
        var result = await _controller.Edit(1, _cancionesList[0]) as RedirectToActionResult;
        Assert.AreEqual("Index", result!.ActionName);
    }

    [TestMethod]
    public async Task DeleteConfirmed_ShouldRedirectToIndex()
    {
        var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;
        Assert.AreEqual("Index", result!.ActionName);
    }
}