using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;
using PrulariaAankoopUI.Models;

namespace PrulariaAankoopUI.Controllers
{
    public class CategorieenController : Controller
    {
        private readonly PrulariaComContext _context;
        private readonly CategorieenService _categorieenService;
        private readonly ArtikelenService _artikelenService;
        private readonly ICategorieenRepository _categorieenRepository;

        public CategorieenController(PrulariaComContext context, CategorieenService categorieenService,
            ArtikelenService artikelenService, ICategorieenRepository categorieenRepository)
        {
            _context = context;
            _categorieenService = categorieenService;
            _artikelenService = artikelenService;
            _categorieenRepository = categorieenRepository;
        }

        // GET: Categorieen
        public async Task<IActionResult> Index()
        {
            var categorieViewModel = new CategorieViewModel();
            var lijstCategorieen = await _categorieenService.IndexService();
            categorieViewModel.Categorieen = lijstCategorieen;
            return View(categorieViewModel);
        }

        // GET: Categorieen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            //var categorie = await _categorieenService.GetCategorieByIdAsync(id.Value);// ORIGINELE
            var categorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync((int)id);//NIEUWE
            if (categorie == null)
                return NotFound();

            // Get alle Artikelen nog niet gelinkt aan de huidige Categorie
            var beschikbareArtikelen = 
                await _artikelenService.GetNietGekoppeldeArtikelsVoorCategorieAsync(categorie.CategorieId);

            var dropdownItems = beschikbareArtikelen.Select(a => new SelectListItem
            {
                Value = a.ArtikelId.ToString(),
                Text = a.Naam
            }).ToList();

            var viewModel = new CategorieViewModel
            {
                CategorieId = categorie.CategorieId,
                Naam = categorie.Naam,
                HoofdCategorieId = categorie.HoofdCategorieId,
                HoofdCategorie = categorie.HoofdCategorie,
                Subcategorieën = categorie.Subcategorieën.ToList(),

                ArtikelToevoegenForm = new CategorieArtikelViewModel
                {
                    CategorieId = categorie.CategorieId,
                    CategorieNaam = categorie.Naam,
                    BeschikbareArtikelen = dropdownItems
                }
            };

            var overigeCategorieen = await _categorieenService.GetOverigeCategorieen2Async((int)id);
            ViewData["LijstOverigeCategorieId"] = new SelectList(overigeCategorieen, "CategorieId", "Naam");

            var subCategorieen =  categorie.Subcategorieën;
            //var subCategorieen = await _categorieenService.GetSubCategorieënAsync((int)id);
            //var subCategorieen = await _categorieenService.GetOverigeCategorieen2Async((int)id);
            ViewData["LijstSubCategorieenId"] = new SelectList(subCategorieen, "CategorieId", "Naam");

           

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var hoofdCategorien = await _categorieenService.GetHoofdCategorien();
            var categorieToevoegenViewModel = new CategorieToevoegenViewModel()
            {
                HoofdCategorien = new SelectList(hoofdCategorien, "CategorieId", "Naam")
            };
            return View(categorieToevoegenViewModel);
        }

        // POST: Categorieen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategorieToevoegenViewModel categorieToevoegenViewModel)
        {
            if (ModelState.IsValid)
            {
                bool categorieAlBestaat = await _categorieenService.CategorieMetNaamAlBestaat(categorieToevoegenViewModel.Naam);
                if (categorieAlBestaat)
                {
                    ModelState.AddModelError("Naam", "Categorie met dezelfde naam bestaat al. Geef een andere naam");
                }
                else
                {
                    var nieuweCategorie = new Categorie()
                    {
                        Naam = categorieToevoegenViewModel.Naam,
                        HoofdCategorieId = categorieToevoegenViewModel.HoofdCategorieId,
                    };
                    await _categorieenService.CategorieToevoegen(nieuweCategorie);
                    TempData["Boodschap"] = "Categorie werd toegevoegd.";
                    return RedirectToAction(nameof(Index));
                }
            }
            var hoofdCategorien = await _categorieenService.GetHoofdCategorien();
            categorieToevoegenViewModel.HoofdCategorien = new SelectList(hoofdCategorien, "CategorieId", "Naam");
            return View(categorieToevoegenViewModel);
        }

        // GET: Categorieen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieenService.GetCategorieByIdAsync(id.Value);
            if (categorie == null)
            {
                TempData["Melding"] = "Categorie niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            return View(categorie);
        }

        // POST: Categorieen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategorieId,Naam,HoofdCategorieId")] Categorie categorie)
        {
            if (id != categorie.CategorieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bestaandeCategorie = await _categorieenService.GetCategorieByIdAsync(id);
                    if (bestaandeCategorie != null)
                    {
                        await _categorieenService.HernoemCategorieAsync(id, categorie.Naam);
                        TempData["Melding"] = "De categorie is succesvol hernoemd!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    return BadRequest("Er is iets fout gegaan.");
                }
            }

            return View(categorie);
        }

        // GET: Categorieen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _categorieenService.GetCategorieByIdAsync(id.Value);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // POST: Categorieen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorie = await _categorieenService.GetCategorieByIdAsync(id);

            if (categorie == null)
            {
                TempData["Melding"] = "Categorie niet gevonden.";
                return RedirectToAction(nameof(Index));
            }

            // Controleer of de categorie leeg is
            bool kanVerwijderdWorden = await _categorieenService.KanVerwijderdWordenAsync(id);

            if (!kanVerwijderdWorden)
            {
                return View("DeleteFailed", categorie); // Toon foutpagina
            }

            bool success = await _categorieenService.VerwijderCategorieAsync(id);

            if (success)
            {
                return View("DeleteSuccess"); // Toon succespagina
            }

            TempData["Melding"] = "Er is een fout opgetreden bij het verwijderen van de categorie.";
            return RedirectToAction(nameof(Index));
        
        }

        private bool CategorieExists(int id)
        {
            return _context.Categorieen.Any(e => e.CategorieId == id);
        }

        [HttpGet]
        public async Task<IActionResult> VoegArtikelToe(int categorieId)
        {
            var categorie = await _categorieenService.GetCategorieByIdAsync(categorieId);
            if (categorie == null) return NotFound();

            var beschikbareArtikels = await _categorieenService.GetNietGekoppeldeArtikelsVoorCategorieAsync(categorieId);

            var model = new CategorieArtikelViewModel
            {
                CategorieId = categorieId,
                CategorieNaam = categorie.Naam,
                BeschikbareArtikelen = beschikbareArtikels
                    .Select(a => new SelectListItem { Value = a.ArtikelId.ToString(), Text = a.Naam })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KoppelArtikelAanCategorie(CategorieViewModel model)
        {
            var form = model.ArtikelToevoegenForm;

            if (form.ArtikelId == 0)
            {
                TempData["ErrorMessage"] = "Selecteer een geldig artikel.";
                return RedirectToAction("Details", new { id = form.CategorieId });
            }

            bool success = await _categorieenService.AddArtikelAanCategorieAsync(form.ArtikelId, form.CategorieId);

            TempData["SuccessMessage"] = success
                ? "Artikel succesvol gekoppeld."
                : "Artikel is mogelijk al gekoppeld of koppeling is mislukt.";

            return RedirectToAction("Details", new { id = form.CategorieId });
        }


        // A.1500.Lesley
        // BevestigCategorieToevoegen
        [HttpPost]
        public async Task<IActionResult> BevestigCategorieToevoegen(int categorieId, int gekozenCategorieId)
        {
            try
            {
                var categorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(categorieId);

                var gekozenCategorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(gekozenCategorieId);

                if (categorie == null || gekozenCategorie == null) return NotFound();

                var categorieCategorieViewModel = new CategorieCategorieViewModel
                {
                    Categorie = categorie,
                    GekozenCategorie = gekozenCategorie
                };

                return View(categorieCategorieViewModel);
            }
            catch (Exception)
            {
                return StatusCode(500, "Er is een interne fout opgetreden.");
            }


        }



        // A.1500.Lesley
        // CategorieToevoegenAanCategorie
        [HttpPost]
        public async Task<IActionResult> CategorieToevoegenAanCategorie(int categorieId, int gekozenCategorieId)
        {
            try
            {


                var categorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(categorieId);

                var gekozenCategorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(gekozenCategorieId);




                if (categorie == null || gekozenCategorie == null)
                    return NotFound();

                categorie.Subcategorieën.Add(gekozenCategorie);
                await _categorieenRepository.SaveChangesAsync();

                return RedirectToAction("Details", new { id = categorie.CategorieId });
            }
            catch (Exception)
            {
                return StatusCode(500, "Er is een interne fout opgetreden.");
            }
        }

        // A.1500.Lesley
        // BevestigCategorieVerwijderen
        [HttpPost]
        public async Task<IActionResult> BevestigCategorieVerwijderen(int categorieId, int gekozenCategorieId)
        {
            try
            {
                var categorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(categorieId);
                var gekozenCategorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(gekozenCategorieId);

                if (categorie == null || gekozenCategorie == null) return NotFound();

                var categorieCategorieViewModel = new CategorieCategorieViewModel
                {
                    Categorie = categorie,
                    GekozenCategorie = gekozenCategorie
                };

                return View(categorieCategorieViewModel);
            }
            catch (Exception)
            {
                return StatusCode(500, "Er is een interne fout opgetreden.");
            }
        }




        // A.1500.Lesley
        // CategorieVerwijderenVanCategorie
        [HttpPost]
public async Task<IActionResult> CategorieVerwijderenVanCategorie(int categorieId, int? gekozenCategorieId)
{
    try
    {
        var categorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(categorieId);
        var gekozenCategorie = await _categorieenService.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync((int)gekozenCategorieId);

        if (categorie == null || gekozenCategorie == null)
            return NotFound();

        if (categorie.Subcategorieën.Contains(gekozenCategorie))
        {


            await _categorieenService.HoofdcategorieIdOpNullZettenAsync((int)gekozenCategorieId);

            return RedirectToAction("Details", new { id = categorie.CategorieId });

        }

        return RedirectToAction("Details", new { id = categorie.CategorieId });
    }
    catch (Exception)
    {
        return StatusCode(500, "Er is een interne fout opgetreden.");
    }
}





        // A.1300.Lesley
        // BevestigArtikelVerwijderen
        [HttpGet]
        public async Task<IActionResult> BevestigArtikelVerwijderen(int artikelId, int categorieId)
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

        // A.1300.Lesley
        // ArtikelVerwijderenVanCategorie
        [HttpPost]
        public async Task<IActionResult> ArtikelVerwijderenVanCategorie(ArtikelCategorieViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(string.Join(", ", errors));
                }

                var categorie = await _categorieenService.GetCategorieByIdAsync(model.CategorieId);
                if (categorie == null)
                    return BadRequest("Categorie niet gevonden.");

                bool success = await _categorieenService.RemoveArtikelVanCategorieAsync(model.ArtikelId, categorie);

                if (!success)
                    return BadRequest("Fout bij verwijderen van het artikel uit de categorie.");


                return RedirectToAction("Details", new { id = model.CategorieId });
            }
            catch (Exception)
            {
                return StatusCode(500, "Er is een interne fout opgetreden.");
            }
        }



    }
}
