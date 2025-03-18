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

        public ArtikelenController(PrulariaComContext context, ArtikelenService artikelService)
        {
            _context = context;
            _artikelenService = artikelService;
        }

        // GET: Artikelen
        public async Task<IActionResult> Index(ArtikelViewModel form)
        {
            ViewData["Categorie"] = new SelectList(_context.Categorieen, "CategorieId", "Naam");
            return View(await _artikelenService.MaakGefilterdeLijstArtikelen(form));
        }

        // GET: Artikelen/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var artikel = await _context.Artikelen.FindAsync(id);
            if (artikel == null)
            {
                return NotFound();
            }
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);
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
                return NotFound();
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
    }
}
