using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;
using PrulariaAankoopUI.Components;
using PrulariaAankoopUI.Models;

namespace PrulariaAankoopUI.Controllers
{
    public class ActiecodesController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly ActiecodesService _actiecodesService;
        public ActiecodesController(PrulariaComContext context, ActiecodesService actiecodesService)
        {
            _context = context;
            _actiecodesService = actiecodesService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var actiecodes = await _actiecodesService.ToListAsync();
                return View(actiecodes);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // NIEUWE CODE ------------------------------------------------------------------------

       

        // GET: Actiecodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiecode = await _actiecodesService
                .FirstOrDefaultAsync(m => m.ActiecodeId == id);
            if (actiecode == null)
            {
                return NotFound();
            }

            return View(actiecode);
        }


        // GET: Actiecodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actiecodes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NieuweActiecodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // als de actiecode nog niet in de database bestaat:
                if (_actiecodesService.IsActieCodeNieuw(model.Naam, model.GeldigVanDatum, model.GeldigTotDatum ))
                {
                    var actiecode = new Actiecode()
                    {
                        Naam = model.Naam,
                        GeldigVanDatum = model.GeldigVanDatum,
                        GeldigTotDatum = model.GeldigTotDatum,
                        IsEenmalig = model.IsEenmalig,
                    };
                    await _actiecodesService.RegistrerenActiecodeAsync(actiecode);
                    return RedirectToAction("Index");
                }
                // als de actiecode in de database WEL bestaat:
                else
                {
                    ViewBag.bestaandeActiecode = "De Actiecode bestaat al in de database";
                }
            }
            return View(model);
        }

        // GET: Actiecodes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var actiecode = await _context.Actiecodes.FirstOrDefaultAsync(a => a.ActiecodeId == id);
            if (actiecode == null)
            {
                return NotFound();
            }

            var model = new ActiecodeWijzigenViewModel
            {
                Id = actiecode.ActiecodeId,
                Naam = actiecode.Naam,
                GeldigVanDatum = actiecode.GeldigVanDatum,
                GeldigTotDatum = actiecode.GeldigTotDatum,
                IsEenmalig = actiecode.IsEenmalig,
                IsEdit = true,
                OrigineleBegindatum = actiecode.GeldigVanDatum
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ActiecodeWijzigenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var actiecode = await _actiecodesService.FindAsync(model.Id);
                if (actiecode == null)
                {
                    return NotFound();
                }

                actiecode.Naam = model.Naam;
                actiecode.GeldigVanDatum = model.GeldigVanDatum;
                actiecode.GeldigTotDatum = model.GeldigTotDatum;
                actiecode.IsEenmalig = model.IsEenmalig;

                try
                {
                    await _actiecodesService.UpdateAsync(actiecode);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Er is een fout opgetreden tijdens het verwerken van uw aanvraag. Probeer het later nog eens.");
                    return View(model);
                }
                ViewBag.bevestiging = "De actiecode \"" + model.Naam + "\" is gewijzigd";
                return View(model);
            }

            // Als de validatie mislukt:
            return View(model);

        }

        // GET: Actiecodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actiecode = await _actiecodesService
                .FirstOrDefaultAsync(m => m.ActiecodeId == id);
            if (actiecode == null)
            {
                return NotFound();
            }

            return View(actiecode);
        }

        // POST: Actiecodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actiecode = await _actiecodesService.FindAsync(id);
            if (actiecode != null)
            {
                _actiecodesService.Remove(actiecode);
            }

            await _actiecodesService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiecodeExists(int id)
        {
            return _actiecodesService.Any(e => e.ActiecodeId == id);
        }


    }
}
