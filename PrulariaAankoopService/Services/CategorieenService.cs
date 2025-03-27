using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;

public class CategorieenService
{
    private readonly ICategorieenRepository _categorieenRepository;
    public CategorieenService(ICategorieenRepository categorieenRepository)
    {
        _categorieenRepository = categorieenRepository;
    }
    public async Task<List<Categorie>> IndexService()
    {
        return (await _categorieenRepository.GetLijstCategorieen());
    }

    public async Task<List<Categorie>> GetAlleCategorieenAsync()
    {
        return await _categorieenRepository.GetAlleCategorieenAsync();
    }

    public async Task<Categorie?> GetCategorieByIdAsync(int id)
    {
        return await _categorieenRepository.GetByIdAsync(id);
    }
    public async Task<List<Categorie>> GetOverigeCategorieenAsync(int artikelId)
    {
        return await _categorieenRepository.GetOverigeCategorieenAsync(artikelId);
    }




    // Get all categories
    public async Task<IEnumerable<Categorie>> GetAllCategorieenAsync()
    {
        return await _categorieenRepository.GetAllCategorieenAsync();
    }

    // Rename a category by its ID
    public async Task HernoemCategorieAsync(int categorieId, string nieuweNaam)
    {
        await _categorieenRepository.HernoemCategorieAsync(categorieId, nieuweNaam);
    }
    public async Task<bool> KanVerwijderdWordenAsync(int id)
    {
        var categorie = await _categorieenRepository.GetCategorieMetRelatiesByIdAsync(id);
        return categorie != null && !categorie.Subcategorieën.Any() && !categorie.Artikelen.Any();
    }

    public async Task<bool> VerwijderCategorieAsync(int id)
    {
        return await _categorieenRepository.DeleteCategorieAsync(id);
    }
}

