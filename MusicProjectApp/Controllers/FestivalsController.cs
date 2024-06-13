using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicProjectApp.Models;

namespace MusicProjectApp.Controllers
{
    public class FestivalsController(GrupoAContext context) : Controller
    {
        // GET: Festivals
        public async Task<IActionResult> Index()
        {
            var grupoAContext = context.Festival.Include(f => f.Artista);
            return View(await grupoAContext.ToListAsync());
        }

        // GET: Festivals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await context.Festival
                .Include(f => f.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // GET: Festivals/Create
        public IActionResult Create()
        {
            ViewData["ArtistaId"] = new SelectList(context.Artistas, "Id", "Nombre");
            return View();
        }

        // POST: Festivals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,ArtistaId,Ciudad,FechaInicio,FechaFinal")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                context.Add(festival);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistaId"] = new SelectList(context.Artistas, "Id", "Nombre", festival.ArtistaId);
            return View(festival);
        }

        // GET: Festivals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await context.Festival.FindAsync(id);
            if (festival == null)
            {
                return NotFound();
            }
            ViewData["ArtistaId"] = new SelectList(context.Artistas, "Id", "Nombre", festival.ArtistaId);
            return View(festival);
        }

        // POST: Festivals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // "For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,ArtistaId,Ciudad,FechaInicio,FechaFinal")] Festival festival)
        {
            if (id != festival.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(festival);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FestivalExists(festival.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistaId"] = new SelectList(context.Artistas, "Id", "Nombre", festival.ArtistaId);
            return View(festival);
        }

        // GET: Festivals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await context.Festival
                .Include(f => f.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // POST: Festivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var festival = await context.Festival.FindAsync(id);
            if (festival != null)
            {
                context.Festival.Remove(festival);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FestivalExists(int id)
        {
            return context.Festival.Any(e => e.Id == id);
        }
    }
}
