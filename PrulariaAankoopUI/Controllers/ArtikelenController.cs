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
    public class ArtikelenController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly ArtikelenService _artikelenService;

        public ArtikelenController(PrulariaComContext context, ArtikelenService artikelenService)
        {
            _context = context;
            _artikelenService = artikelenService;
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
            try
            {

                if (artikel == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return View(artikel);
        }

        // GET: Artikelen/Create
        public IActionResult Create()
        {
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam");
            return View();
        }

        // POST: Artikelen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artikel artikel)
        {
            // Uiteindelijk vervangen met methode van Leveranciersrepository/service
            artikel.Leverancier = await _context.Leveranciers.FindAsync(artikel.LeveranciersId);

            if (_artikelenService.CheckOfArtikelBestaat(artikel))
                ModelState.AddModelError("Naam", "Een artikel met deze naam en beschrijving bestaat al.");

            if (this.ModelState.IsValid)
            {
                await _artikelenService.AddArtikel(artikel);
                return RedirectToAction(nameof(Index));
            }
            // Uiteindelijk vervangen met methode van Leveranciersrepository/service
            ViewBag.LeveranciersId = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam", artikel.LeveranciersId);
            return View(artikel);
        }

        // GET: Artikelen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artikel = await _artikelenService.GetArtikelById(id.Value);
            if (artikel == null)
            {
                return NotFound();
            }
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam", artikel.Artikel.LeveranciersId);
            return View(artikel);
        }

        // POST: Artikelen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtikelViewModel artikelViewModel)
        {
            if (id != artikelViewModel.Artikel.ArtikelId)
            {
                return NotFound();
            }
            var artikelCheck = await _artikelenService.GetArtikelById(id);
            if (artikelCheck == null)
            {
                return NotFound();
            }
            Artikel artikel = artikelCheck.Artikel;
            artikel = artikelViewModel.Artikel;
            _artikelenService.UpdateArtikel(artikel);
            ViewBag.Message = "Artikel succesvol gewijzigd.";
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam", artikelViewModel.Artikel.LeveranciersId);
            return View(artikelViewModel);
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
        // GET: Artikelen/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Artikelen/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string ean)
        {
            var artikel = await _context.Artikelen
                .Include(a => a.Leverancier)
                .FirstOrDefaultAsync(a => a.Ean == ean);

            if (artikel == null)
            {
                ViewBag.Message = "Artikel niet gevonden.";
                return View();
            }

            var artikelViewModel = new ArtikelViewModel
            {
                ArtikelId = artikel.ArtikelId,
                Ean = artikel.Ean,
                Naam = artikel.Naam,
                Beschrijving = artikel.Beschrijving,
                Prijs = artikel.Prijs,
                GewichtInGram = artikel.GewichtInGram,
                Bestelpeil = artikel.Bestelpeil,
                Voorraad = artikel.Voorraad,
                MinimumVoorraad = artikel.MinimumVoorraad,
                MaximumVoorraad = artikel.MaximumVoorraad,
                Levertijd = artikel.Levertijd,
                AantalBesteldLeverancier = artikel.AantalBesteldLeverancier,
                MaxAantalInMagazijnPlaats = artikel.MaxAantalInMagazijnPlaats,
                LeveranciersId = artikel.LeveranciersId
            };

            return View("Edit", artikelViewModel);
        }

        public IActionResult Filter(ArtikelViewModel form)
        {
            return RedirectToAction("Index", form);
        }

        [HttpGet]
        public async Task<IActionResult> BevestigSetNonActief(int artikelId)
        {
            var artikel = await _artikelenService.GetByIdAsync(artikelId);
            if (artikel == null)
            {
                return NotFound();
            }

            var artikelViewModel = new ArtikelViewModel
            {
                ArtikelId = artikel.ArtikelId,
                Naam = artikel.Naam,
                Beschrijving = artikel.Beschrijving
            };

            return View(artikelViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetArtikelNonActief(int artikelId)
        {
            try
            {
                await _artikelenService.SetArtikelNonActiefAsync(artikelId);
                TempData["SuccessMessage"] = "Artikel is succesvol op non-actief gezet.";
                return RedirectToAction("Details", new { id = artikelId });
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "Het artikel is al gewijzigd door een andere gebruiker." +
                    " Probeer het opnieuw.";
                return RedirectToAction("Details", new { id = artikelId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Fout bij op non-actief zetten: {ex.Message}";
                return RedirectToAction("Details", new { id = artikelId });
            }
        }
    }
}
