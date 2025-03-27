using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;
using PrulariaAankoopUI.Models; 


namespace PrulariaAankoopUI.Controllers
{
    public class ArtikelenController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly ArtikelenService _artikelenService;
        private readonly CategorieenService _categorieenService;

        public ArtikelenController(PrulariaComContext context, ArtikelenService artikelenService, CategorieenService categorieenService)
        {
            _context = context;
            _artikelenService = artikelenService;
            _categorieenService = categorieenService;
        }

        // GET: Artikelen
        public async Task<IActionResult> Index(ArtikelViewModel form)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
                return View(await _artikelenService.MaakGefilterdeLijstArtikelen(form));
            return RedirectToAction("Index", "Home");
        }

        // GET: Artikelen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
                        throw new Exception($"Artikel met ID {id} werd niet gevonden.");
                    }
                    ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);

                    //var categorieen = await _categorieenService.GetAlleCategorieenAsync();
                    var categorieen = await _categorieenService.GetOverigeCategorieenAsync((int)id);

                    ViewData["CategorieId"] = new SelectList(categorieen, "CategorieId", "Naam"); // "Naam" moet overeenkomen met je model

                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return View(artikel);
            }
            return RedirectToAction("Index", "Home");
        }




        // GET: Artikelen/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam");
                return View();
            }
            return RedirectToAction("Index", "Home");
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
            if (HttpContext.Session.GetString("Ingelogd") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var artikel = await _artikelenService.GetArtikelById(id.Value);
                var artikelViewModel = new ArtikelViewModel();
                artikelViewModel.Artikel = artikel;
                if (artikel == null)
                {
                    return NotFound();
                }
                ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam", artikelViewModel.Artikel.LeveranciersId);
                return View(artikelViewModel);
            }
            return RedirectToAction("Index", "Home");
        }


        //***********************************************************************************************

        //// POST: Artikelen/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ArtikelId,Ean,Naam,Beschrijving,Prijs,GewichtInGram,Bestelpeil,Voorraad,MinimumVoorraad,MaximumVoorraad,Levertijd,AantalBesteldLeverancier,MaxAantalInMagazijnPlaats,LeveranciersId")] Artikel artikel)
        //{
        //    if (id != artikel.ArtikelId)
        //    {
        //        return NotFound();
        //    }

        //    // Error boodschap zegt wat er mist om de modelstate.IsValid te doen slagen
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        //        return BadRequest(string.Join(", ", errors));
        //    }

        //    if (ModelState.IsValid)
        //    {

        //        try
        //        {
        //            _context.Update(artikel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ArtikelExists(artikel.ArtikelId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "BtwNummer", artikel.LeveranciersId);
        //    return View(artikel);

        //}


        // POST: Artikelen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtikelViewModel artikelViewModel)
        {
            if (id != artikelViewModel.Artikel.ArtikelId)
            {
                return NotFound();
            }
            // Ensure the price is rounded to 2 decimal places before saving
            artikelViewModel.Artikel.Prijs = Math.Round(artikelViewModel.Artikel.Prijs, 2);
            await _artikelenService.UpdateArtikel(artikelViewModel);
            ViewBag.Message = "Artikel succesvol gewijzigd.";
            ViewData["LeveranciersId"] = new SelectList(_context.Leveranciers, "LeveranciersId", "Naam", artikelViewModel.Artikel.LeveranciersId);
            return View(artikelViewModel);
        }



        // GET: Artikelen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Ingelogd") != null)
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
            return RedirectToAction("Index", "Home");
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


        // Lesley
        // BevestigCategorieToevoegen
        [HttpPost]
        public async Task<IActionResult> BevestigCategorieToevoegen(int artikelId, int categorieId)
        {
            try
            {

                var artikel = await _context.Artikelen.FindAsync(artikelId);
            var categorie = await _context.Categorieen.FindAsync(categorieId);

            if (artikel == null || categorie == null) return NotFound();

            var viewModel = new ArtikelCategorieViewModel
            {
                ArtikelId = artikelId,
                ArtikelNaam = artikel.Naam,
                CategorieId = categorieId,
                CategorieNaam = categorie.Naam
            };

            return View(viewModel);
        }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Er is een interne fout opgetreden.");
    }
}

        // Lesley
        // CategorieToevoegenAanArtikel
        [HttpPost]
        public async Task<IActionResult> CategorieToevoegenAanArtikel(ArtikelCategorieViewModel model)
        {
        try
        {
        
        if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(string.Join(", ", errors));
            }
            var categorie = await _categorieenService.GetCategorieByIdAsync(model.CategorieId);

            bool success = await _artikelenService.AddCategorieAanArtikelAsync(model.ArtikelId, categorie );

            if (!success)
                return BadRequest("Fout bij toevoegen van de categorie.");

            return RedirectToAction("Details", new { id = model.ArtikelId });
        }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Er is een interne fout opgetreden.");
    }
}

        // Lesley
        // BevestigCategorieVerwijderen
        [HttpGet]
        public async Task<IActionResult> BevestigCategorieVerwijderen(int artikelId, int categorieId)
        {
            try
            {
                var artikel = await _context.Artikelen.FindAsync(artikelId);
            var categorie = await _context.Categorieen.FindAsync(categorieId);

            if (artikel == null || categorie == null) return NotFound();

            var viewModel = new ArtikelCategorieViewModel
            {
                ArtikelId = artikelId,
                ArtikelNaam = artikel.Naam,
                CategorieId = categorieId,
                CategorieNaam = categorie.Naam
            };

            return View(viewModel);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Er is een interne fout opgetreden.");
            }
        }

        // Lesley
        // CategorieToevoegenAanArtikel
        [HttpPost]
        public async Task<IActionResult> CategorieVerwijderenVanArtikel(ArtikelCategorieViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(string.Join(", ", errors));
            }
            var categorie = await _categorieenService.GetCategorieByIdAsync(model.CategorieId);

            bool success = await _artikelenService.RemoveCategorieVanArtikelAsync(model.ArtikelId, categorie);

            if (!success)
                return BadRequest("Fout bij Verwijderen van de categorie.");

            return RedirectToAction("Details", new { id = model.ArtikelId });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Er is een interne fout opgetreden.");
            }


}





    }
}
