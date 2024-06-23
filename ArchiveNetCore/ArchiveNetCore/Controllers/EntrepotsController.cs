using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArchiveNetCore.Data;
using ArchiveNetCore.Models;

namespace ArchiveNetCore.Controllers
{
    public class EntrepotsController : Controller
    {
        private readonly ArchiveNetCoreContext _context;

        public EntrepotsController(ArchiveNetCoreContext context)
        {
            _context = context;
        }

        // GET: Entrepots
        public async Task<IActionResult> Index()
        {
              return _context.Entrepots != null ? 
                          View(await _context.Entrepots.ToListAsync()) :
                          Problem("Entity set 'ArchiveNetCoreContext.Entrepots'  is null.");
        }

        // GET: Entrepots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entrepots == null)
            {
                return NotFound();
            }

            var entrepots = await _context.Entrepots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrepots == null)
            {
                return NotFound();
            }

            return View(entrepots);
        }

        // GET: Entrepots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entrepots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,libelle,nbre_rayon,nbre_niveau,nbre_range,desc")] Entrepots entrepots)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entrepots);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entrepots);
        }

        // GET: Entrepots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entrepots == null)
            {
                return NotFound();
            }

            var entrepots = await _context.Entrepots.FindAsync(id);
            if (entrepots == null)
            {
                return NotFound();
            }
            return View(entrepots);
        }

        // POST: Entrepots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,libelle,nbre_rayon,nbre_niveau,nbre_range,desc")] Entrepots entrepots)
        {
            if (id != entrepots.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrepots);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrepotsExists(entrepots.Id))
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
            return View(entrepots);
        }

        // GET: Entrepots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entrepots == null)
            {
                return NotFound();
            }

            var entrepots = await _context.Entrepots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrepots == null)
            {
                return NotFound();
            }

            return View(entrepots);
        }

        // POST: Entrepots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entrepots == null)
            {
                return Problem("Entity set 'ArchiveNetCoreContext.Entrepots'  is null.");
            }
            var entrepots = await _context.Entrepots.FindAsync(id);
            if (entrepots != null)
            {
                _context.Entrepots.Remove(entrepots);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntrepotsExists(int id)
        {
          return (_context.Entrepots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
