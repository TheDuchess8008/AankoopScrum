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
            var categorieViewModel = new CategorieViewModel();
            var lijstCategorieen = await _categorieenService.IndexService();
            categorieViewModel.Categorieen = lijstCategorieen;
            return View(categorieViewModel);
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var hoofdCategorien = await _categorieenService.GetHoofdCategorien();
            var categorieToevoegenViewModel = new CategorieToevoegenViewModel()
            {
                HoofdCategorien = new SelectList(hoofdCategorien, "CategorieId", "Naam")
            };
            return View(categorieToevoegenViewModel);
        }

        // POST: Categorieen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategorieToevoegenViewModel categorieToevoegenViewModel)
        {
            if (ModelState.IsValid)
            {
                bool categorieAlBestaat = await _categorieenService.CategorieMetNaamAlBestaat(categorieToevoegenViewModel.Naam);
                if (categorieAlBestaat)
                {
                    ModelState.AddModelError("Naam", "Categorie met dezelfde naam bestaat al. Geef een andere naam");
                }
                else
                {
                    var nieuweCategorie = new Categorie()
                    {
                        Naam = categorieToevoegenViewModel.Naam,
                        HoofdCategorieId = categorieToevoegenViewModel.HoofdCategorieId,
                    };
                    await _categorieenService.CategorieToevoegen(nieuweCategorie);
                    TempData["Boodschap"] = "Categorie werd toegevoegd.";
                    return RedirectToAction(nameof(Index));
                }
            }
            var hoofdCategorien = await _categorieenService.GetHoofdCategorien();
            categorieToevoegenViewModel.HoofdCategorien = new SelectList(hoofdCategorien, "CategorieId", "Naam");
            return View(categorieToevoegenViewModel);
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
