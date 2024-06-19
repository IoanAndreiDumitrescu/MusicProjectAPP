using Microsoft.AspNetCore.Mvc;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectApp.Controllers
{
    public class AlbumesController : Controller
    {
        private readonly IGenericRepositorio<Albumes> _repo;

        public AlbumesController(IGenericRepositorio<Albumes> repo)
        {
            _repo = repo;
        }

        private async Task<Albumes?> GetVerifiedAlbum(int? id)
        {
            if (id == null)
                return null;

            var album = await _repo.DameUno(id.Value);
            return album;
        }

        // GET: Albumes
        public async Task<IActionResult> Index(string searchString)
        {
            Expression<Func<Albumes, bool>> filterExpression;

            if (!String.IsNullOrEmpty(searchString))
            {
                filterExpression = a => a.Titulo.StartsWith(searchString);
            }
            else
            {
                filterExpression = a => true;
            }
            var albums = await _repo.Filtra(filterExpression);
            return View(albums);
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var album = await GetVerifiedAlbum(id);
            if (album == null) return NotFound();

            return View(album);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albumes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Genero,Fecha,Titulo")] Albumes album)
        {
            if (ModelState.IsValid)
            {
                await _repo.Agregar(album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var album = await GetVerifiedAlbum(id);
            if (album == null) return NotFound();

            return View(album);
        }

        // POST: Albumes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Genero,Fecha,Titulo")] Albumes album)
        {
            if (id != album.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repo.Modificar(album.Id, album);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var album = await GetVerifiedAlbum(id);
            if (album == null) return NotFound();

            return View(album);        
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Borrar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}