using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using System.Linq.Expressions;

namespace MusicProjectApp.Controllers
{
    public class CancionesController(
        IGenericRepositorio<Canciones> cancionesRepo,
        IGenericRepositorio<Albumes> albumesRepo,
        IGenericRepositorio<Artistas>? artistasRepo)
        : Controller
    {
        // GET: Canciones
        public async Task<IActionResult> Index(string? searchString)
        {
            Expression<Func<Canciones, bool>> filterExpression;

            if (!String.IsNullOrEmpty(searchString))
            {
                filterExpression = a => a.Titulo.StartsWith(searchString);
            }
            else
            {
                filterExpression = a => true;
            }
            var canciones = await cancionesRepo.Filtra(filterExpression);

            return View(canciones);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await cancionesRepo.DameUno(id.Value);

            if (canciones == null)
            {
                return NotFound();
            }

            return View(canciones);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AlbumId"] = new SelectList(await albumesRepo.DameTodos(), "Id", "Titulo");
            ViewData["ArtistaId"] = new SelectList(await artistasRepo!.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Canciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,ArtistaId,AlbumId")] Canciones canciones)
        {
            if (ModelState.IsValid)
            {
                await cancionesRepo.Agregar(canciones);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(await albumesRepo.DameTodos(), "Id", "Titulo", canciones.AlbumId);
            ViewData["ArtistaId"] = new SelectList(await artistasRepo!.DameTodos(), "Id", "Nombre", canciones.ArtistaId);
            return View(canciones);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await cancionesRepo.DameUno(id.Value);
            if (canciones == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(await albumesRepo.DameTodos(), "Id", "Titulo", canciones.AlbumId);
            ViewData["ArtistaId"] = new SelectList(await artistasRepo!.DameTodos(), "Id", "Nombre", canciones.ArtistaId);
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
                    await cancionesRepo.Modificar(id, canciones);
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
            ViewData["AlbumId"] = new SelectList(await albumesRepo.DameTodos(), "Id", "Titulo", canciones.AlbumId);
            ViewData["ArtistaId"] = new SelectList(await artistasRepo!.DameTodos(), "Id", "Nombre", canciones.ArtistaId);
            return View(canciones);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await cancionesRepo.DameUno(id.Value);
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
            await cancionesRepo.Borrar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancionesExists(int id)
        {
            var canciones = await cancionesRepo.DameUno(id);
            return canciones != null;
        }
    }
}