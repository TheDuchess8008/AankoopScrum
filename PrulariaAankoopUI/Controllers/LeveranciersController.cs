using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopUI.Controllers
{
    public class LeveranciersController : Controller
    {
        private readonly PrulariaComContext _context;

        public LeveranciersController(PrulariaComContext context)
        {
            _context = context;
        }

        // GET: Leveranciers
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                var prulariaComContext = _context.Leveranciers.Include(l => l.Plaats);
                return View(await prulariaComContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Leveranciers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Leveranciers/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam");
                return View();
            }
            return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var leverancier = await _context.Leveranciers.FindAsync(id);
                if (leverancier == null)
                {
                    return NotFound();
                }
                ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam", leverancier.PlaatsId);
                return View(leverancier);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Leveranciers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeveranciersId,Naam,BtwNummer,Straat,HuisNummer,Bus,PlaatsId,FamilienaamContactpersoon,VoornaamContactpersoon")] Leverancier leverancier)
        {
            if (id != leverancier.LeveranciersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leverancier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeverancierExists(leverancier.LeveranciersId))
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
            ViewData["PlaatsId"] = new SelectList(_context.Plaatsen, "PlaatsId", "Naam", leverancier.PlaatsId);
            return View(leverancier);
        }

        // GET: Leveranciers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
            return RedirectToAction("Index", "Home");
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
