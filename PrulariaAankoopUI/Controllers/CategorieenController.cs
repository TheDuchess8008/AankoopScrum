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
using PrulariaAankoopUI.Models;

namespace PrulariaAankoopUI.Controllers
{
    public class CategorieenController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly CategorieenService _categorieenService;

        public CategorieenController(PrulariaComContext context, CategorieenService categorieenService)
        {
            _context = context;
            _categorieenService = categorieenService;
        }

        // GET: Categorieen
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                var categorieViewModel = new CategorieViewModel();
                var lijstCategorieen = await _categorieenService.IndexService();
                categorieViewModel.Categorieen = lijstCategorieen;
                return View(categorieViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Categorieen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Categorieen/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                ViewData["HoofdCategorieId"] = new SelectList(_context.Categorieen, "CategorieId", "Naam");
                return View();
            }
            return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
            return RedirectToAction("Index", "Home");
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
                    _context.Update(categorie);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoofdCategorieId"] = new SelectList(_context.Categorieen, "CategorieId", "Naam", categorie.HoofdCategorieId);
            return View(categorie);
        }

        // GET: Categorieen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
            return RedirectToAction("Index", "Home");
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
