using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProjectApp.Models;
using System.Linq.Expressions;

namespace MusicProjectApp.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly IGenericRepositorio<Artistas> _repo;

        public ArtistasController(IGenericRepositorio<Artistas> repo)
        {
            _repo = repo;
        }

        // GET: Artistas
        public async Task<IActionResult> Index(string searchString)
        {
            Expression<Func<Artistas, bool>> filterExpression;

            if (!String.IsNullOrEmpty(searchString))
            {
                filterExpression = a => a.Nombre.Contains(searchString);
            }
            else
            {
                filterExpression = a => true;
            }
            var artista = await _repo.Filtra(filterExpression);

            return View(artista);
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            Artistas artista = await _repo.DameUno(id.Value);
            if (artista == null) return NotFound();

            return View(artista);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artistas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Genero,Fecha")] Artistas artista)
        {
            if (ModelState.IsValid)
            {
                _repo.Agregar(artista);
                return RedirectToAction("Index");
            }
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Artistas artista = await _repo.DameUno(id.Value);
            if (artista == null) return NotFound();

            return View(artista);
        }

        // POST: Artistas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Genero,Fecha")] Artistas artista)
        {
            if (id != artista.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _repo.Modificar(artista.Id, artista);
                return RedirectToAction("Index");
            }

            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Artistas artista = await _repo.DameUno(id.Value);
            if (artista == null) return NotFound();

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await _repo.Borrar(id);
            return RedirectToAction("Index");
        }
    }
}