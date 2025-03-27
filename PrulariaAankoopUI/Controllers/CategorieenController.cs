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
        private readonly ArtikelenService _artikelenService;
        public CategorieenController(PrulariaComContext context, CategorieenService categorieenService,
            ArtikelenService artikelenService)
        {
            _context = context;
            _categorieenService = categorieenService;
            _artikelenService = artikelenService;
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

            // Get alle Artikelen nog niet gelinkt aan de huidige Categorie
            var beschikbareArtikelen = 
                await _artikelenService.GetNietGekoppeldeArtikelsVoorCategorieAsync(categorie.CategorieId);

            var dropdownItems = beschikbareArtikelen.Select(a => new SelectListItem
            {
                Value = a.ArtikelId.ToString(),
                Text = a.Naam
            }).ToList();

            var viewModel = new CategorieViewModel
            {
                CategorieId = categorie.CategorieId,
                Naam = categorie.Naam,
                HoofdCategorieId = categorie.HoofdCategorieId,
                HoofdCategorie = categorie.HoofdCategorie,
                Subcategorieën = categorie.Subcategorieën.ToList(),

                ArtikelToevoegenForm = new CategorieArtikelViewModel
                {
                    CategorieId = categorie.CategorieId,
                    CategorieNaam = categorie.Naam,
                    BeschikbareArtikelen = dropdownItems
                }
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

        [HttpGet]
        public async Task<IActionResult> VoegArtikelToe(int categorieId)
        {
            var categorie = await _categorieenService.GetCategorieByIdAsync(categorieId);
            if (categorie == null) return NotFound();

            var beschikbareArtikels = await _categorieenService.GetNietGekoppeldeArtikelsVoorCategorieAsync(categorieId);

            var model = new CategorieArtikelViewModel
            {
                CategorieId = categorieId,
                CategorieNaam = categorie.Naam,
                BeschikbareArtikelen = beschikbareArtikels
                    .Select(a => new SelectListItem { Value = a.ArtikelId.ToString(), Text = a.Naam })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KoppelArtikelAanCategorie(CategorieViewModel model)
        {
            var form = model.ArtikelToevoegenForm;

            if (form.ArtikelId == 0)
            {
                TempData["ErrorMessage"] = "Selecteer een geldig artikel.";
                return RedirectToAction("Details", new { id = form.CategorieId });
            }

            bool success = await _categorieenService.AddArtikelAanCategorieAsync(form.ArtikelId, form.CategorieId);

            TempData["SuccessMessage"] = success
                ? "Artikel succesvol gekoppeld."
                : "Artikel is mogelijk al gekoppeld of koppeling is mislukt.";

            return RedirectToAction("Details", new { id = form.CategorieId });
        }
    }
}
