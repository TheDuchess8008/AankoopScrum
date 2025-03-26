using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopService.Services;

public class CategorieenService
{
    private readonly ICategorieenRepository _categorieRepository;

    public CategorieenService(ICategorieenRepository categorieRepository)
    {
        _categorieRepository = categorieRepository;
    }

    public async Task<Categorie?> GetCategorieByIdAsync(int id)
    {
        return await _categorieRepository.GetByIdAsync(id);
    }
}