using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PrulariaAankoop.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;

namespace PrulariaAankoopUI.Components
{
    public class ArtikelsPerSubcategorieViewComponent : ViewComponent
    {
        private readonly CategorieenService _categorieService;
        private readonly IArtikelenRepository _artikelRepository;

        public ArtikelsPerSubcategorieViewComponent(CategorieenService categorieService, IArtikelenRepository artikelRepository)
        {
            _categorieService = categorieService;
            _artikelRepository = artikelRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categorieId, string? zoekterm = null)
        {
            var viewModel = await GetArtikelsPerSubcategorieAsync(categorieId, zoekterm?.Trim());
            return View(viewModel);
        }

        public async Task<List<ArtikelsPerSubcategorieViewModel>> GetArtikelsPerSubcategorieAsync(int categorieId, string? zoekterm)
        {
            var categorie = await _categorieService.GetCategorieByIdAsync(categorieId);
            if (categorie == null) return new();

            var result = new List<ArtikelsPerSubcategorieViewModel>();

            // Haal artikels op uit deze categorie
            var directeArtikels = await _artikelRepository.GetArtikelsByCategorieIdAsync(categorieId, zoekterm);
            if (directeArtikels.Any())
            {
                result.Add(new ArtikelsPerSubcategorieViewModel
                {
                    SubcategorieNaam = "Artikels in deze categorie",
                    HeeftSubcategorieën = categorie.Subcategorieën.Any(),
                    Artikels = directeArtikels.Select(a => new ArtikelShortViewModel
                    {
                        Naam = a.Naam ?? "(Geen naam)",
                        Beschrijving = a.Beschrijving ?? "(Geen beschrijving)",

                        ArtikelId = a.ArtikelId, // NIEUW
                        CategorieId = categorieId // NIEUW
                    }).ToList()
                });
            }

            // Haal artikels uit subcategorieën
            if (categorie.Subcategorieën.Any())
            {
                var subcats = categorie.Subcategorieën;

                

                foreach (var sub in subcats.OrderBy(s => s.Naam))
                {
                    var artikelsInSub = await _artikelRepository.GetArtikelsByCategorieIdAsync(sub.CategorieId, zoekterm); // NIEUW 

                    // ORIGINEEL (Terugzetten ?)
                    //var artikelsInSub = sub.Artikelen
                    //    .Where(a => string.IsNullOrEmpty(zoekterm) ||
                    //                (!string.IsNullOrEmpty(a.Naam) &&
                    //                 a.Naam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)))
                    //    .OrderBy(a => a.Naam)
                    //    .Select(a => new ArtikelShortViewModel
                    //    {
                    //        Naam = a.Naam ?? "(Geen naam)",
                    //        Beschrijving = a.Beschrijving ?? "(Geen beschrijving)"
                    //    })
                    //    .ToList();

                    if (artikelsInSub.Any())
                    {
                        result.Add(new ArtikelsPerSubcategorieViewModel
                        {
                            SubcategorieNaam = sub.Naam,
                            HeeftSubcategorieën = true,
                            //Artikels = artikelsInSub

                            // NIEUW
                            Artikels = artikelsInSub.Select(a => new ArtikelShortViewModel
                            {
                                Naam = a.Naam ?? "(Geen naam)",
                                Beschrijving = a.Beschrijving ?? "(Geen beschrijving)",
                                ArtikelId = a.ArtikelId,
                                CategorieId = sub.CategorieId // Zorg ervoor dat CategorieId correct wordt ingesteld
                            }).ToList() // NIEUW




                        });
                    }
                }
            }
            return result;
        }
    }
}
