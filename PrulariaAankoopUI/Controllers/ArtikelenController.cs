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
        public async Task<IActionResult> Index()
        {
            var prulariaComContext = _context.Artikelen.Include(a => a.Leverancier);
            return View(await prulariaComContext.ToListAsync());
        }

        // GET: Artikelen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var artikel = await _artikelenService.GetArtikelById(id.Value);
                return View(artikel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        // GET: Artikelen/Create
        public IActionResult Create()
        {
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer");
            return View();
        }

        // POST: Artikelen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var artikel = await _artikelenService.GetArtikelById(id.Value);
            if (artikel == null)
            {
                return NotFound();
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
                LeveranciersId = artikel.LeveranciersId,
                LeverancierNaam = artikel.Leverancier.Naam // Assuming you want to show the supplier's name
            };

            return View(artikelViewModel);
        }

        // POST: Artikelen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtikelId,Ean,Naam,Beschrijving,Prijs,GewichtInGram,Bestelpeil,Voorraad,MinimumVoorraad,MaximumVoorraad,Levertijd,AantalBesteldLeverancier,MaxAantalInMagazijnPlaats,LeveranciersId")] ArtikelViewModel artikelViewModel)
        {
            if (id != artikelViewModel.ArtikelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var artikel = await _context.Artikelen.FindAsync(id);
                if (artikel == null)
                {
                    return NotFound();
                }

                artikel.Ean = artikelViewModel.Ean;
                artikel.Naam = artikelViewModel.Naam;
                artikel.Beschrijving = artikelViewModel.Beschrijving;
                artikel.Prijs = artikelViewModel.Prijs;
                artikel.GewichtInGram = artikelViewModel.GewichtInGram;
                artikel.Bestelpeil = artikelViewModel.Bestelpeil;
                artikel.Voorraad = artikelViewModel.Voorraad;
                artikel.MinimumVoorraad = artikelViewModel.MinimumVoorraad;
                artikel.MaximumVoorraad = artikelViewModel.MaximumVoorraad;
                artikel.Levertijd = artikelViewModel.Levertijd;
                artikel.AantalBesteldLeverancier = artikelViewModel.AantalBesteldLeverancier;
                artikel.MaxAantalInMagazijnPlaats = artikelViewModel.MaxAantalInMagazijnPlaats;
                artikel.LeveranciersId = artikelViewModel.LeveranciersId;

                _context.Update(artikel);
                await _context.SaveChangesAsync();

                ViewBag.Message = "Artikel succesvol gewijzigd.";
                return View(artikelViewModel);
            }

            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikelViewModel.LeveranciersId);
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

    }
}
