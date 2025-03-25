using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;

namespace PrulariaAankoopUI.Controllers
{
    public class CategorieenController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly CategorieenService _categorieenService;

        public CategorieenController(PrulariaComContext context)
        {
            _context = context;
        }

        // GET: Categorieen
        public async Task<IActionResult> Index()
        {
            var prulariaComContext = _context.Categorieen.Include(c => c.HoofdCategorie);
            return View(await prulariaComContext.ToListAsync());
        }

        // GET: Categorieen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorieen
                .Include(c => c.HoofdCategorie)
                .FirstOrDefaultAsync(m => m.CategorieId == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // GET: Categorieen/Create
        public IActionResult Create()
        {
            ViewData["HoofdCategorieId"] = new SelectList(_context.Categorieen, "CategorieId", "Naam");
            return View();
        }

        // POST: Categorieen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategorieId,Naam,HoofdCategorieId")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoofdCategorieId"] = new SelectList(_context.Categorieen, "CategorieId", "Naam", categorie.HoofdCategorieId);
            return View(categorie);
        }

        // GET: Categorieen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorieen.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }
            ViewData["HoofdCategorieId"] = new SelectList(_context.Categorieen, "CategorieId", "Naam", categorie.HoofdCategorieId);
            return View(categorie);
        }

        // POST: Categorieen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategorieId,Naam,HoofdCategorieId")] Categorie categorie)
        {
            if (id != categorie.CategorieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categorieNaamUpdate = await _context.Categorieen.FindAsync(id);
                    if (categorieNaamUpdate != null)
                    {
                        categorieNaamUpdate.Naam = categorie.Naam; // Rename logic integrated here
                        _context.Update(categorieNaamUpdate);      // Update the entity
                        await _context.SaveChangesAsync();
                        TempData["Melding"] = "De categorie is succesvol hernoemd!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorieExists(categorie.CategorieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["HoofdCategorieId"] = new SelectList(_context.Categorieen, "CategorieId", "Naam", categorie.HoofdCategorieId);
            return View(categorie);
        }

        // GET: Categorieen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorieen
                .Include(c => c.HoofdCategorie)
                .FirstOrDefaultAsync(m => m.CategorieId == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // POST: Categorieen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorie = await _context.Categorieen.FindAsync(id);
            if (categorie != null)
            {
                _context.Categorieen.Remove(categorie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorieExists(int id)
        {
            return _context.Categorieen.Any(e => e.CategorieId == id);
        }
    }
}
