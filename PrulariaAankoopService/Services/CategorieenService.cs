using PrulariaAankoopData.Models;
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

    public async Task<Categorie> GetCategorieByIdAsync(int id)
    {
        return await _categorieenRepository.GetCategorieByIdAsync(id);
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

}
