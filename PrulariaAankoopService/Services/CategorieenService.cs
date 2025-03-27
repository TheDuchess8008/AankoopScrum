using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public async Task<List<Artikel>> GetNietGekoppeldeArtikelsVoorCategorieAsync(int categorieId)
    {
        return await _artikelenRepository.GetNietGekoppeldeArtikelsVoorCategorieAsync(categorieId);
    }

    public async Task<bool> AddArtikelAanCategorieAsync(int artikelId, int categorieId)
    {
        return await _artikelenRepository.AddArtikelAanCategorieAsync(artikelId, categorieId);
    }

}