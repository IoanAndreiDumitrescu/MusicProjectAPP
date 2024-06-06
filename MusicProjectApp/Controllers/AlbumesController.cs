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
    public class AlbumesController : Controller
    {
        private readonly GrupoAContext _context;

        public AlbumesController(GrupoAContext context)
        {
            _context = context;
        }

        // GET: Albumes
        // GET: Albumes
        public async Task<IActionResult> Index(string searchString)
        {
            var albums = from a in _context.Albumes
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(a => a.Titulo.Contains(searchString));
            }

            return View(await albums.ToListAsync());
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = await _context.Albumes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albumes == null)
            {
                return NotFound();
            }

            return View(albumes);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Genero,Fecha,Titulo")] Albumes albumes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(albumes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(albumes);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = await _context.Albumes.FindAsync(id);
            if (albumes == null)
            {
                return NotFound();
            }
            return View(albumes);
        }

        // POST: Albumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Genero,Fecha,Titulo")] Albumes albumes)
        {
            if (id != albumes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(albumes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumesExists(albumes.Id))
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
            return View(albumes);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = await _context.Albumes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albumes == null)
            {
                return NotFound();
            }

            return View(albumes);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumes = await _context.Albumes.FindAsync(id);
            if (albumes != null)
            {
                _context.Albumes.Remove(albumes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumesExists(int id)
        {
            return _context.Albumes.Any(e => e.Id == id);
        }
    }
}
