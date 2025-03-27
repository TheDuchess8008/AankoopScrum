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
                return NotFound();

            var categorie = await _categorieenService.GetCategorieByIdAsync(id.Value);
            if (categorie == null)
                return NotFound();

            var viewModel = new CategorieViewModel
            {
                CategorieId = categorie.CategorieId,
                Naam = categorie.Naam,
                HoofdCategorieId = categorie.HoofdCategorieId,
                HoofdCategorie = categorie.HoofdCategorie,
                Subcategorieën = categorie.Subcategorieën.ToList()
            };

            return View(viewModel);
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

            var categorie = await _categorieenService.GetCategorieByIdAsync(id.Value);
            if (categorie == null)
            {
                TempData["Melding"] = "Categorie niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            return View(categorie);
        }

        // POST: Categorieen/Edit/5
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
                    var bestaandeCategorie = await _categorieenService.GetCategorieByIdAsync(id);
                    if (bestaandeCategorie != null)
                    {
                        await _categorieenService.HernoemCategorieAsync(id, categorie.Naam);
                        TempData["Melding"] = "De categorie is succesvol hernoemd!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    return BadRequest("Er is iets fout gegaan.");
                }
            }

            return View(categorie);
        }

        // GET: Categorieen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieenService.GetCategorieByIdAsync(id.Value);
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
            var categorie = await _categorieenService.GetCategorieByIdAsync(id);

            if (categorie == null)
            {
                TempData["Melding"] = "Categorie niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            // Controleer of de categorie leeg is
            bool kanVerwijderdWorden = await _categorieenService.KanVerwijderdWordenAsync(id);

            if (!kanVerwijderdWorden)
            {
                return View("DeleteFailed", categorie); // Toon foutpagina
            }

            bool success = await _categorieenService.VerwijderCategorieAsync(id);

            if (success)
            {
                return View("DeleteSuccess"); // Toon succespagina
            }

            TempData["Melding"] = "Er is een fout opgetreden bij het verwijderen van de categorie.";
            return RedirectToAction(nameof(Index));
        
        }

        private bool CategorieExists(int id)
        {
            return _context.Categorieen.Any(e => e.CategorieId == id);
        }
    }
}
