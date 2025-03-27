using System.Collections.Generic;
using System.Threading.Tasks;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopService.Services
{
    public class CategorieenService
    {
        private readonly ICategorieenRepository _categorieenRepository;

        public CategorieenService(ICategorieenRepository categorieenRepository)
        {
            _categorieenRepository = categorieenRepository;
        }

        // Get a category by its ID
        public async Task<Categorie?> GetCategorieByIdAsync(int id)
        {
            return await _categorieenRepository.GetCategorieByIdAsync(id);
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
}
