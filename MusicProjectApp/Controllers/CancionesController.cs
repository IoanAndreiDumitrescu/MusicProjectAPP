using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicProjectApp.Models;
using System.Threading.Tasks;

namespace MusicProjectApp.Controllers
{
    public class CancionesController : Controller
    {
        private readonly IGenericRepositorio<Canciones> _cancionesRepo;
        private readonly IGenericRepositorio<Albumes> _albumesRepo;
        private readonly IGenericRepositorio<Artistas> _artistasRepo;

        public CancionesController(IGenericRepositorio<Canciones> cancionesRepo, IGenericRepositorio<Albumes> albumesRepo, IGenericRepositorio<Artistas> artistasRepo)
        {
            _cancionesRepo = cancionesRepo;
            _albumesRepo = albumesRepo;
            _artistasRepo = artistasRepo;
        }

        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            return View(await _cancionesRepo.DameTodos());
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _cancionesRepo.DameUno(id.Value);
            if (canciones == null)
            {
                return NotFound();
            }

            return View(canciones);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AlbumId"] = new SelectList(await _albumesRepo.DameTodos(), "Id", "Titulo");
            ViewData["ArtistaId"] = new SelectList(await _artistasRepo.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Canciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,ArtistaId,AlbumId")] Canciones canciones)
        {
            if (ModelState.IsValid)
            {
                await _cancionesRepo.Agregar(canciones);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(await _albumesRepo.DameTodos(), "Id", "Titulo", canciones.AlbumId);
            ViewData["ArtistaId"] = new SelectList(await _artistasRepo.DameTodos(), "Id", "Nombre", canciones.ArtistaId);
            return View(canciones);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _cancionesRepo.DameUno(id.Value);
            if (canciones == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(await _albumesRepo.DameTodos(), "Id", "Titulo", canciones.AlbumId);
            ViewData["ArtistaId"] = new SelectList(await _artistasRepo.DameTodos(), "Id", "Nombre", canciones.ArtistaId);
            return View(canciones);
        }

        // POST: Canciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,ArtistaId,AlbumId")] Canciones canciones)
        {
            if (id != canciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cancionesRepo.Modificar(id, canciones);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await CancionesExists(canciones.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["AlbumId"] = new SelectList(await _albumesRepo.DameTodos(), "Id", "Titulo", canciones.AlbumId);
            ViewData["ArtistaId"] = new SelectList(await _artistasRepo.DameTodos(), "Id", "Nombre", canciones.ArtistaId);
            return View(canciones);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _cancionesRepo.DameUno(id.Value);
            if (canciones == null)
            {
                return NotFound();
            }

            return View(canciones);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cancionesRepo.Borrar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancionesExists(int id)
        {
            var canciones = await _cancionesRepo.DameUno(id);
            return canciones != null;
        }
    }
}