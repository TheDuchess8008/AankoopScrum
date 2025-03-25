using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopService.Services;
public class CategorieenService
{
    private readonly ICategorieenRepository _categorieenRepository;
    public CategorieenService(ICategorieenRepository categorieenRepository)
    {
        _categorieenRepository = categorieenRepository;
    }
    public async Task HernoemCategorieAsync(int categorieId, string nieuweNaam)
    {
        await _categorieenRepository.HernoemCategorieAsync(categorieId, nieuweNaam);
    }
}
