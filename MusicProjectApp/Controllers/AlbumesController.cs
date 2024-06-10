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
            if (id == null) return NotFound();

            Albumes album = await _repo.DameUno(id.Value);
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
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Albumes album = await _repo.DameUno(id.Value);
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
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Albumes album = await _repo.DameUno(id.Value);
            if (album == null) return NotFound();

            return View(album);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _repo.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}

