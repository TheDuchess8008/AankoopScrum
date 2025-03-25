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
        private readonly ILeveranciersRepository _leveranciersRepository;
        private readonly LeveranciersService _leveranciersService;
        private readonly PrulariaComContext _context;
        
        public LeveranciersController(PrulariaComContext context, ILeveranciersRepository leveranciersRepository, LeveranciersService leveranciersService)
        {
            _context = context;
            _leveranciersRepository = leveranciersRepository;
            _leveranciersService = leveranciersService; 
        }

        // GET: Leveranciers
        public async Task<IActionResult> Index()
        {
            var leveranciers = await _leveranciersRepository.GetAllLeveranciersAsync();

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
            // Load all unique postcodes
            var postcodes = _context.Plaatsen
                .Select(p => p.Postcode)
                .Distinct()
                .ToList();

            var plaatsen = _context.Plaatsen
                .Select(p => new SelectListItem
                {
                    Value = p.PlaatsId.ToString(),
                    Text = p.Naam
                })
                .ToList();

            var model = new NieuweLeverancierViewModel
            {
                // Populate the Postcode dropdown
                Postcodes = postcodes.Select(p => new SelectListItem
                {
                    Text = p,
                    Value = p
                }).ToList(),

                // Populate the Plaatsen dropdown with all places
                Plaatsen = plaatsen
            };

            return View(model);
        }

        // POST: Leveranciers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NieuweLeverancierViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate if the selected Plaats matches the selected Postcode
                var selectedPlaats = await _context.Plaatsen
                    .FirstOrDefaultAsync(p => p.PlaatsId == model.PlaatsId);

                if (selectedPlaats == null || selectedPlaats.Postcode != model.SelectedPostcode)
                {
                    // Add a model error if the Plaats does not match the selected Postcode
                    ModelState.AddModelError("PlaatsId", "De geselecteerde plaats komt niet overeen met de gekozen postcode.");
                }

                if (ModelState.IsValid)
                {
                    // Create a new Leverancier
                    var leverancier = new Leverancier
                    {
                        Naam = model.Naam,
                        BtwNummer = model.BtwNummer,
                        Straat = model.Straat,
                        HuisNummer = model.HuisNummer,
                        Bus = model.Bus,
                        PlaatsId = model.PlaatsId,  // (foreign key)
                        FamilienaamContactpersoon = model.FamilienaamContactpersoon,
                        VoornaamContactpersoon = model.VoornaamContactpersoon
                    };

                    // Add the new leverancier to the database
                    _context.Leveranciers.Add(leverancier);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            // If validation failed, repopulate the dropdowns and return the view
            var postcodes = _context.Plaatsen
                .Select(p => p.Postcode)
                .Distinct()
                .ToList();

            var plaatsen = _context.Plaatsen
                .Select(p => new SelectListItem
                {
                    Value = p.PlaatsId.ToString(),
                    Text = p.Naam
                })
                .ToList();

            model.Postcodes = postcodes.Select(p => new SelectListItem
            {
                Text = p,
                Value = p
            }).ToList();

            model.Plaatsen = plaatsen;

            return View(model);
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
