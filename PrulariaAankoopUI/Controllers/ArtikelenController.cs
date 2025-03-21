using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class ArtikelenController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly ArtikelenService _artikelenService;
        private readonly CategorieenService _categorieenService;

        public ArtikelenController(PrulariaComContext context, ArtikelenService artikelenService, CategorieenService categorieenService)
        {
            _context = context;
            _artikelenService = artikelenService;
            _categorieenService = categorieenService;
        }

        // GET: Artikelen
        public async Task<IActionResult> Index(ArtikelViewModel form)
        {
            return View(await _artikelenService.MaakGefilterdeLijstArtikelen(form));
        }

        // GET: Artikelen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artikel = await _artikelenService.MaakDetailsArtikel((int)id);
            if (artikel == null)
            {
                throw new Exception($"Artikel met ID {id} werd niet gevonden.");
            }
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);
            
            var categorieen = await _categorieenService.GetAlleCategorieenAsync();

            ViewData["CategorieId"] = new SelectList(categorieen, "CategorieId", "Naam"); // "Naam" moet overeenkomen met je model


            return View(artikel);
        }




        // GET: Artikelen/Create
        public IActionResult Create()
        {
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer");
            return View();
        }

        // POST: Artikelen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtikelId,Ean,Naam,Beschrijving,Prijs,GewichtInGram,Bestelpeil,Voorraad,MinimumVoorraad,MaximumVoorraad,Levertijd,AantalBesteldLeverancier,MaxAantalInMagazijnPlaats,LeveranciersId")] Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artikel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);
            return View(artikel);
        }

        // GET: Artikelen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.Artikelen.FindAsync(id);
            if (artikel == null)
            {
                return NotFound();
            }
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);
            return View(artikel);
        }

        // POST: Artikelen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtikelId,Ean,Naam,Beschrijving,Prijs,GewichtInGram,Bestelpeil,Voorraad,MinimumVoorraad,MaximumVoorraad,Levertijd,AantalBesteldLeverancier,MaxAantalInMagazijnPlaats,LeveranciersId")] Artikel artikel)
        {
            if (id != artikel.ArtikelId)
            {
                return NotFound();
            }

            // Error boodschap zegt wat er mist om de modelstate.IsValid te doen slagen
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(string.Join(", ", errors));
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    _context.Update(artikel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtikelExists(artikel.ArtikelId))
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
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);
            return View(artikel);
        }

        // GET: Artikelen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikel = await _context.Artikelen
                .Include(a => a.Leverancier)
                .FirstOrDefaultAsync(m => m.ArtikelId == id);
            if (artikel == null)
            {
                return NotFound();
            }

            return View(artikel);
        }

        // POST: Artikelen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikel = await _context.Artikelen.FindAsync(id);
            if (artikel != null)
            {
                _context.Artikelen.Remove(artikel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtikelExists(int id)
        {
            return _context.Artikelen.Any(e => e.ArtikelId == id);
        }

        public IActionResult Filter(ArtikelViewModel form)
        {
            return RedirectToAction("Index", form);
        }

        //--------------------------------------------------------------------------------------------
        // NIEUW



        [HttpGet]
        public async Task<IActionResult> BevestigCategorieToevoegen(int artikelId, int categorieId)
        {
            var artikel = await _context.Artikelen.FindAsync(artikelId);
            var categorie = await _context.Categorieen.FindAsync(categorieId);

            if (artikel == null || categorie == null) return NotFound();

            var viewModel = new BevestigCategorieToevoegenViewModel
            {
                ArtikelId = artikelId,
                ArtikelNaam = artikel.Naam,
                CategorieId = categorieId,
                CategorieNaam = categorie.Naam
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CategorieToevoegenAanArtikel(BevestigCategorieToevoegenViewModel model)
        {
            // Error boodschap zegt wat er mist om de modelstate.IsValid te doen slagen
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(string.Join(", ", errors));
            }
            var categorie = await _categorieenService.GetCategorieByIdAsync(model.CategorieId);

            bool success = await _artikelenService.AddCategorieAanArtikelAsync(model.ArtikelId, categorie );

            if (!success)
                return BadRequest("Fout bij toevoegen van de categorie.");

            return RedirectToAction("Details", new { id = model.ArtikelId });
        }




    }
}
