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
}
