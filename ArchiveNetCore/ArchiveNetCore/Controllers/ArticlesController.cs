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
    public class ArticlesController : Controller
    {
        private readonly ArchiveNetCoreContext _context;

        public ArticlesController(ArchiveNetCoreContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
              return _context.Articles != null ? 
                          View(await _context.Articles.ToListAsync()) :
                          Problem("Entity set 'ArchiveNetCoreContext.Articles'  is null.");
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,libelle,niv,rayon,range,idEntrepot,desc")] Articles articles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articles);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,libelle,niv,rayon,range,idEntrepot,desc")] Articles articles)
        {
            if (id != articles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticlesExists(articles.Id))
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
            return View(articles);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'ArchiveNetCoreContext.Articles'  is null.");
            }
            var articles = await _context.Articles.FindAsync(id);
            if (articles != null)
            {
                _context.Articles.Remove(articles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticlesExists(int id)
        {
          return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> ArticleNonPlace(int idEntrepots, int niv, int rayon, int range)
        {
            Entrepots entr = await _context.Entrepots.FindAsync(idEntrepots);
            if (entr != null)
            {
                ViewData["idEntrepots"] = idEntrepots;
                ViewData["libelle"] = entr.libelle;
                ViewData["niv"] = niv;
                ViewData["rayon"] = rayon;
                ViewData["range"] = range;
            }
            var articles = await _context.Articles.Where(e => e.idEntrepot == null).ToListAsync();
            return View(articles.Any() ? articles : new List<Articles>());
        }


        [HttpPost]
        public ActionResult MiseEnPlaceArticle(int ref_entrepot, string rayon, string range, string niv, int[] art_selected)
        {
            if (art_selected != null && art_selected.Length > 0)
            {
                // Retrieve the selected articles from the database
                var articles = _context.Articles.Where(a => art_selected.Contains(a.Id)).ToList();

                foreach (var article in articles)
                {
                    article.idEntrepot = ref_entrepot;
                    article.rayon = int.Parse(rayon);
                    article.range = int.Parse(range);
                    article.niv = int.Parse(niv);
                    // Update other fields if necessary
                }

                // Save changes to the database
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetailsArtNivRayRang(int idEntrepots, int niv, int rayon, int range)
        {
            var liste = await _context.Articles
                .Where(a => a.idEntrepot == idEntrepots && a.niv == niv && a.rayon == rayon && a.range == range)
                .ToListAsync();
            return View(liste.Any() ? liste : new List<Articles>());
            //return Json(liste);
        }
    }
}
