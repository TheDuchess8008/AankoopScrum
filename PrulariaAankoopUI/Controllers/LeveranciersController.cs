using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopUI.Models;

namespace PrulariaAankoopUI.Controllers
{
    public class LeveranciersController : Controller
    {
        private readonly ILeverancierRepository _leverancierRepository;
        private readonly PrulariaComContext _context;

        public LeveranciersController(PrulariaComContext context, ILeverancierRepository leverancierRepository)
        {
            _context = context;
            _leverancierRepository = leverancierRepository; 
        }

        // GET: Leveranciers
        public async Task<IActionResult> Index()
        {
            var leveranciers = await _leverancierRepository.GetAllLeveranciersAsync();

            var viewModel = leveranciers.Select(l => new LeverancierViewModel
            {
                LeveranciersId = l.LeveranciersId,
                Naam = l.Naam,
                BtwNummer = l.BtwNummer,
                Straat = l.Straat,
                HuisNummer = l.HuisNummer,
                Bus = l.Bus,
                PlaatsId = l.PlaatsId,
                FamilienaamContactpersoon = l.FamilienaamContactpersoon,
                VoornaamContactpersoon = l.VoornaamContactpersoon,
                Plaats = l.Plaats
            }).ToList();

            return View(viewModel);
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
