using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoop.Models;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopUI.Components;
public class ArtikelsPerSubcategorieViewComponent : ViewComponent
{
    private readonly ICategorieenRepository _categorieenRepository;
    private readonly IArtikelenRepository _artikelenRepository;

    public ArtikelsPerSubcategorieViewComponent(ICategorieenRepository categorieenRepository, IArtikelenRepository artikelenRepository)
    {
        _categorieenRepository = categorieenRepository;
        _artikelenRepository = artikelenRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(int categorieId, string? zoekterm = null)
    {
        zoekterm = zoekterm?.Trim();

        var categorie = await _categorieenRepository.GetByIdAsync(categorieId);
        if (categorie == null)
            return View(new List<ArtikelsPerSubcategorieViewModel>());

        var viewModel = new List<ArtikelsPerSubcategorieViewModel>();

        if (categorie.Subcategorieën.Any())
        {
            foreach (var sub in categorie.Subcategorieën.OrderBy(s => s.Naam))
            {
                var artikels = await _artikelenRepository.GetArtikelsByCategorieIdAsync(sub.CategorieId, zoekterm);

                if (artikels.Any())
                {
                    viewModel.Add(new ArtikelsPerSubcategorieViewModel
                    {
                        SubcategorieNaam = sub.Naam,
                        HeeftSubcategorieën = true,
                        Artikels = artikels.Select(a => new ArtikelShortViewModel
                        {
                            Naam = a.Naam ?? "(Geen naam)",
                            Beschrijving = a.Beschrijving ?? "(Geen beschrijving)"
                        }).ToList()
                    });
                }
            }
        }
        else
        {
            var artikels = await _artikelenRepository.GetArtikelsByCategorieIdAsync(categorieId, zoekterm);
            if (artikels.Any())
            {
                viewModel.Add(new ArtikelsPerSubcategorieViewModel
                {
                    HeeftSubcategorieën = false,
                    Artikels = artikels.Select(a => new ArtikelShortViewModel
                    {
                        Naam = a.Naam ?? "(Geen naam)",
                        Beschrijving = a.Beschrijving ?? "(Geen beschrijving)"
                    }).ToList()
                });
            }
        }

        return View(viewModel);
    }
}
