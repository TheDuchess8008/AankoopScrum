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

            // Populate dropdown with leveranciers
            ViewData["LeveranciersId"] = new SelectList(
                _context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId
            );

            return View(artikel);
        }

        // POST: Artikelen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtikelId,Ean,Naam,Beschrijving,Prijs,GewichtInGram,Bestelpeil,Voorraad,MinimumVoorraad,MaximumVoorraad,Levertijd,AantalBesteldLeverancier,MaxAantalInMagazijnPlaats,LeveranciersId")] Artikel artikel)
        {
            if (id != artikel.ArtikelId)
            {
                return BadRequest("Artikel ID komt niet overeen.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["LeveranciersId"] = new SelectList(
                    _context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId
                );
                return View(artikel);
            }

            try
            {
                await _artikelenService.UpdateArtikel(artikel);
                return RedirectToAction(nameof(Details), new { id = artikel.ArtikelId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewData["LeveranciersId"] = new SelectList(
                    _context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId
                );
                return View(artikel);
            }
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


    }
}
