using Microsoft.AspNetCore.Mvc;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectApp.Controllers
{
    public class AlbumesController(IGenericRepositorio<Albumes> repo) : Controller
    {
        private async Task<Albumes?> GetVerifiedAlbum(int? id)
        {
            if (id == null)
                return null;

            var album = await repo.DameUno(id.Value);
            return album;
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            var albums = await GetAlbumsBySearchString(searchString);
            return View(albums);
        }
        
        

        public async Task<IActionResult> AlbumesPorCancion(string searchString)
        {
            var albums = await GetAlbumsBySearchString(searchString);
            return View(albums);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var album = await GetVerifiedAlbum(id);
            if (album == null) return NotFound();
            return View(album);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Genero,Fecha,Titulo")] Albumes album)
        {
            if (ModelState.IsValid)
            {
                await repo.Agregar(album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var album = await GetVerifiedAlbum(id);
            if (album == null) return NotFound();
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Genero,Fecha,Titulo")] Albumes album)
        {
            if (id != album.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await repo.Modificar(album.Id, album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var album = await GetVerifiedAlbum(id);
            if (album == null) return NotFound();
            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repo.Borrar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<Albumes>> GetAlbumsBySearchString(string? searchString)
        {
            Expression<Func<Albumes, bool>> filterExpression;
            if (!String.IsNullOrEmpty(searchString))
            {
                filterExpression = a => a.Titulo!.StartsWith(searchString);
            }
            else
            {
                filterExpression = a => true;
            }
            return await repo.Filtra(filterExpression);
        }
    }
}