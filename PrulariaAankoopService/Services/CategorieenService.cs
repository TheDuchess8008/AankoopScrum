using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoop.Models;

namespace PrulariaAankoopService.Services;

public class CategorieenService
{
    private readonly ICategorieenRepository _categorieRepository;
    private readonly IArtikelenRepository _artikelRepository;

    public CategorieenService(ICategorieenRepository categorieRepository, IArtikelenRepository artikelRepository)
    {
        _categorieRepository = categorieRepository;
        _artikelRepository = artikelRepository;
    }

    public async Task<CategorieViewModel?> GetCategorieViewModelByIdAsync(int id)
    {
        var categorie = await _categorieRepository.GetByIdAsync(id);
        if (categorie == null) return null;

        return new CategorieViewModel
        {
            CategorieId = categorie.CategorieId,
            Naam = categorie.Naam,
            HoofdCategorieId = categorie.HoofdCategorieId,
            HoofdCategorie = categorie.HoofdCategorie,
            Subcategorieën = categorie.Subcategorieën.ToList()
        };
    }

    public async Task<List<ArtikelsPerSubcategorieViewModel>> GetArtikelsPerSubcategorieAsync(int categorieId, string? zoekterm = null)
    {
        var categorie = await _categorieRepository.GetByIdAsync(categorieId);
        if (categorie == null) return new();

        var result = new List<ArtikelsPerSubcategorieViewModel>();

        if (categorie.Subcategorieën.Any())
        {
            var subcats = await _categorieRepository.GetSubcategorieenAsync(categorieId);

            result = subcats
                .OrderBy(s => s.Naam)
                .Select(sub => new ArtikelsPerSubcategorieViewModel
                {
                    SubcategorieNaam = sub.Naam,
                    HeeftSubcategorieën = true,
                    Artikels = sub.Artikelen
                        .Where(a => string.IsNullOrEmpty(zoekterm) || a.Naam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(a => a.Naam)
                        .Select(a => new ArtikelShortViewModel
                        {
                            Naam = a.Naam,
                            Beschrijving = a.Beschrijving
                        }).ToList()
                })
                .Where(vm => vm.Artikels.Any())
                .ToList();
        }
        else
        {
            var artikels = await _artikelRepository.GetArtikelsByCategorieIdAsync(categorieId, zoekterm);
            if (artikels.Any())
            {
                result.Add(new ArtikelsPerSubcategorieViewModel
                {
                    HeeftSubcategorieën = false,
                    Artikels = artikels.Select(a => new ArtikelShortViewModel
                    {
                        Naam = a.Naam,
                        Beschrijving = a.Beschrijving
                    }).ToList()
                });
            }
        }

        return result;
    }
}