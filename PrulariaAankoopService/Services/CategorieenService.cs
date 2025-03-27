using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;

public class CategorieenService
{
    private readonly ICategorieenRepository _categorieenRepository;
    private readonly IArtikelenRepository _artikelenRepository;
    public CategorieenService(ICategorieenRepository categorieenRepository, 
        IArtikelenRepository artikelenRepository)
    {
        _categorieenRepository = categorieenRepository;
        _artikelenRepository = artikelenRepository;
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
    public async Task<bool> CategorieMetNaamAlBestaat(string categorieNaam)
    {
        return await _categorieenRepository.CategorieMetNaamAlBestaat(categorieNaam);
    }

    public async Task CategorieToevoegen(Categorie categorie)
    {
        await _categorieenRepository.CategorieToevoegen(categorie);
    }

    public async Task<List<Categorie>> GetHoofdCategorien()
    {
        return await _categorieenRepository.GetHoofdCategorien();
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
    public async Task<List<Artikel>> GetNietGekoppeldeArtikelsVoorCategorieAsync(int categorieId)
    {
        return await _artikelenRepository.GetNietGekoppeldeArtikelsVoorCategorieAsync(categorieId);
    }

    public async Task<bool> AddArtikelAanCategorieAsync(int artikelId, int categorieId)
    {
        return await _artikelenRepository.AddArtikelAanCategorieAsync(artikelId, categorieId);
    }


    // A.1500.Lesley
    // GetOverigeCategorieenAsync
    public async Task<List<Categorie>> GetOverigeCategorieen2Async(int categorieId)
    {
        return await _categorieenRepository.GetOverigeCategorieen2Async(categorieId);
    }

    // A.1500.Lesley
    // GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync
    public async Task<Categorie> GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(int id)
    {
        return await _categorieenRepository.GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(id);
    }

    // A.1500.Lesley
    // HoofdcategorieIdOpNullZettenAsync
    public async Task HoofdcategorieIdOpNullZettenAsync(int categorieId)
    {
        await _categorieenRepository.HoofdcategorieIdOpNullZettenAsync(categorieId);
    }



}