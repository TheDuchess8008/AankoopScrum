using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopUI.Models;

public class ArtikelsPerSubcategorieViewComponent : ViewComponent
{
    private readonly PrulariaComContext _context;

    public ArtikelsPerSubcategorieViewComponent(PrulariaComContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(int categorieId)
    {
        var categorie = await _context.Categorieen
            .Include(c => c.Subcategorieën)
            .FirstOrDefaultAsync(c => c.CategorieId == categorieId);

        if (categorie == null)
        {
            return View(new List<ArtikelsPerSubcategorieViewModel>());
        }

        var viewModel = new List<ArtikelsPerSubcategorieViewModel>();

        if (categorie.Subcategorieën.Any())
        {
            var subcategorieën = await _context.Categorieen
                .Where(c => c.HoofdCategorieId == categorieId)
                .Include(c => c.Artikelen)
                .ToListAsync();

            viewModel = subcategorieën
                .OrderBy(s => s.Naam)
                .Select(sub => new ArtikelsPerSubcategorieViewModel
                {
                    SubcategorieNaam = sub.Naam,
                    HeeftSubcategorieën = true,
                    Artikels = sub.Artikelen
                        .OrderBy(a => a.Naam)
                        .Select(a => new ArtikelShortViewModel
                        {
                            Naam = a.Naam,
                            Beschrijving = a.Beschrijving
                        }).ToList()
                }).ToList();
        }
        else
        {
            var artikels = await _context.Artikelen
                .Where(a => a.Categorieën.Any(c => c.CategorieId == categorieId))
                .OrderBy(a => a.Naam)
                .ToListAsync();

            viewModel.Add(new ArtikelsPerSubcategorieViewModel
            {
                HeeftSubcategorieën = false,
                Artikels = artikels.Select(a => new ArtikelShortViewModel
                {
                    Naam = a.Naam,
                    Beschrijving = a.Beschrijving
                }).ToList()
            });
        }

        return View(viewModel);
    }
}
