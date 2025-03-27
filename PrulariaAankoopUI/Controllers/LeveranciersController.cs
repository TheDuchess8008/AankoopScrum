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
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                var leveranciers = await _leveranciersService.GetAllLeveranciersAsync();

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
            return RedirectToAction("Index", "Home");
        }

        // GET: Leveranciers/Details/5
        [HttpGet("Leveranciers/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                var leverancier = await _leveranciersService.GetLeverancierByIdAsync(id);
                if (leverancier == null)
                    return NotFound();

                // Converteer naar LeverancierViewModel
                var viewModel = new LeverancierViewModel
                {
                    LeveranciersId = leverancier.LeveranciersId,
                    Naam = leverancier.Naam,
                    BtwNummer = leverancier.BtwNummer,
                    Straat = leverancier.Straat,
                    HuisNummer = leverancier.HuisNummer,
                    Bus = leverancier.Bus,
                    PlaatsId = leverancier.PlaatsId,
                    Plaats = leverancier.Plaats,
                    FamilienaamContactpersoon = leverancier.FamilienaamContactpersoon,
                    VoornaamContactpersoon = leverancier.VoornaamContactpersoon,
                    Artikelen = leverancier.Artikelen
                };

                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Leveranciers/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                // Laad postcodes en plaatsen zoals voorheen, maar via service als nodig
                var model = new NieuweLeverancierViewModel
                {
                    Postcodes = _context.Plaatsen
                            .Select(p => p.Postcode)
                            .Distinct()
                            .Select(p => new SelectListItem { Text = p, Value = p })
                            .ToList(),
                    Plaatsen = _context.Plaatsen
                            .Select(p => new SelectListItem { Value = p.PlaatsId.ToString(), Text = p.Naam })
                            .ToList()
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Leveranciers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NieuweLeverancierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedPlaats = await _context.Plaatsen
                    .FirstOrDefaultAsync(p => p.PlaatsId == model.PlaatsId);

             

                if (ModelState.IsValid)
                {
                    var leverancier = new Leverancier
                    {
                        Naam = model.Naam,
                        BtwNummer = model.BtwNummer,
                        Straat = model.Straat,
                        HuisNummer = model.HuisNummer,
                        Bus = model.Bus,
                        PlaatsId = model.PlaatsId,
                        FamilienaamContactpersoon = model.FamilienaamContactpersoon,
                        VoornaamContactpersoon = model.VoornaamContactpersoon
                    };

                    await _leveranciersService.AddLeverancierAsync(leverancier);

                    return RedirectToAction(nameof(Index));
                }
            }

            // Als de validatie faalt, vul dan de dropdowns opnieuw in
            model.Postcodes = _context.Plaatsen
                            .Select(p => p.Postcode)
                            .Distinct()
                            .Select(p => new SelectListItem { Text = p, Value = p })
                            .ToList();

            model.Plaatsen = _context.Plaatsen
                            .Select(p => new SelectListItem { Value = p.PlaatsId.ToString(), Text = p.Naam })
                            .ToList();

            return View(model);
        }
    


// GET: Leveranciers/Edit/5
public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
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
            return RedirectToAction("Index", "Home");

        }

        // POST: Leveranciers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeveranciersId,Naam,BtwNummer,Straat,HuisNummer,Bus,PlaatsId,FamilienaamContactpersoon,VoornaamContactpersoon")] Leverancier leverancier)
        {
           

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
        
        
        private bool LeverancierExists(int id)
        {
            return _context.Leveranciers.Any(e => e.LeveranciersId == id);
        }
    }
}
