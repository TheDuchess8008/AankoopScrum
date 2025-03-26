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
    public class LeveranciersController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly LeveranciersService _leveranciersService;

        public LeveranciersController(PrulariaComContext context, LeveranciersService leveranciersService)
        {
            _context = context;
            _leveranciersService = leveranciersService;
        }

        // GET: Leveranciers
        public async Task<IActionResult> Index()
        {
            var prulariaComContext = _context.Leveranciers.Include(l => l.Plaats);
            return View(await prulariaComContext.ToListAsync());
        }

        // GET: Leveranciers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leverancier = await _context.Leveranciers
                .Include(l => l.Plaats)
                .FirstOrDefaultAsync(m => m.LeveranciersId == id);
            if (leverancier == null)
            {
                return NotFound();
            }

            return View(leverancier);
        }

        // GET: Leveranciers/Create
        public IActionResult Create()
        {
            ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam");
            return View();
        }

        // POST: Leveranciers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeveranciersId,Naam,BtwNummer,Straat,HuisNummer,Bus,PlaatsId,FamilienaamContactpersoon,VoornaamContactpersoon")] Leverancier leverancier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leverancier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam", leverancier.PlaatsId);
            return View(leverancier);
        }

        // GET: Leveranciers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //var leverancier = await _context.Leveranciers.FirstOrDefaultAsync(a => a.LeveranciersId == id);
            //var leverancier = await _context.Leveranciers
            //.Include(l => l.Plaats)
            //.FirstOrDefaultAsync(a => a.LeveranciersId == id);

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var leverancier = await _leveranciersService.GetLeverancierByIdAsync(id.Value);
            //if (leverancier == null)
            //{
            //    return NotFound();
            //}

            //var model = new LeverancierWijzigenViewModel
            //{
            //    LeveranciersId = leverancier.LeveranciersId,
            //    Naam = leverancier.Naam,
            //    BtwNummer = leverancier.BtwNummer,
            //    Straat = leverancier.Straat,
            //    HuisNummer = leverancier.HuisNummer,
            //    Bus = leverancier.Bus,
            //    PlaatsId = leverancier.PlaatsId,
            //    FamilienaamContactpersoon = leverancier.FamilienaamContactpersoon,
            //    VoornaamContactpersoon = leverancier.VoornaamContactpersoon,
            //    Artikelen = leverancier.Artikelen,
            //    InkomendeLeveringen = leverancier.InkomendeLeveringen,
            //    Plaats = leverancier.Plaats

            //};
            //ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam", leverancier.PlaatsId);
            //return View(model);

            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var leverancier = await _leveranciersService.GetLeverancierByIdAsync(id.Value);
                if (leverancier == null)
                {
                    return NotFound();
                }

                var model = new LeverancierWijzigenViewModel
                {
                    LeveranciersId = leverancier.LeveranciersId,
                    Naam = leverancier.Naam,
                    BtwNummer = leverancier.BtwNummer,
                    Straat = leverancier.Straat,
                    HuisNummer = leverancier.HuisNummer,
                    Bus = leverancier.Bus,
                    PlaatsId = leverancier.PlaatsId,
                    FamilienaamContactpersoon = leverancier.FamilienaamContactpersoon,
                    VoornaamContactpersoon = leverancier.VoornaamContactpersoon
                };

                ViewData["PlaatsId"] = new SelectList(await _leveranciersService.GetPlaatsenAsync(), "PlaatsId", "Naam", leverancier.PlaatsId);
                return View(model);
            }
            catch (Exception ex)
            {
              
                ModelState.AddModelError("", "Er is een fout opgetreden bij het laden van de leverancier.");
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: Leveranciers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeveranciersId,Naam,BtwNummer,Straat,HuisNummer,Bus,PlaatsId,FamilienaamContactpersoon,VoornaamContactpersoon")] Leverancier leverancier)
        {
            //if (id != leverancier.LeveranciersId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(leverancier);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!LeverancierExists(leverancier.LeveranciersId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam", leverancier.PlaatsId);
            //return View(leverancier);

            try
            {
                if (id != leverancier.LeveranciersId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var updatedLeverancier = new Leverancier
                    {
                        LeveranciersId = leverancier.LeveranciersId,
                        Naam = leverancier.Naam,
                        BtwNummer = leverancier.BtwNummer,
                        Straat = leverancier.Straat,
                        HuisNummer = leverancier.HuisNummer,
                        Bus = leverancier.Bus,
                        PlaatsId = leverancier.PlaatsId,
                        FamilienaamContactpersoon = leverancier.FamilienaamContactpersoon,
                        VoornaamContactpersoon = leverancier.VoornaamContactpersoon
                    };

                    bool success = await _leveranciersService.UpdateLeverancierAsync(updatedLeverancier);
                    if (success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "Er is iets misgegaan bij het opslaan.");
                }

                ViewData["PlaatsId"] = new SelectList(await _leveranciersService.GetPlaatsenAsync(), "PlaatsId", "Naam", leverancier.PlaatsId);
                return View(leverancier);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een onverwachte fout opgetreden bij het bijwerken van de leverancier.");
                return View(leverancier);
            }



        }

        // GET: Leveranciers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leverancier = await _context.Leveranciers
                .Include(l => l.Plaats)
                .FirstOrDefaultAsync(m => m.LeveranciersId == id);
            if (leverancier == null)
            {
                return NotFound();
            }

            return View(leverancier);
        }

        // POST: Leveranciers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leverancier = await _context.Leveranciers.FindAsync(id);
            if (leverancier != null)
            {
                _context.Leveranciers.Remove(leverancier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeverancierExists(int id)
        {
            return _context.Leveranciers.Any(e => e.LeveranciersId == id);
        }
    }
}
