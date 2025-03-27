using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories
{
    public class SQLCategorieenRepository : ICategorieenRepository
    {
        private readonly PrulariaComContext _context;

        public SQLCategorieenRepository(PrulariaComContext context)
        {
            _context = context;
        }

        // Get a category by its ID
        public async Task<Categorie?> GetCategorieByIdAsync(int id)
        {
            return await _context.Categorieen.FindAsync(id);
        }

        // Get all categories
        public async Task<IEnumerable<Categorie>> GetAllCategorieenAsync()
        {
            return await _context.Categorieen.Include(c => c.HoofdCategorie).ToListAsync();
        }

        // Rename a category by its ID
        public async Task HernoemCategorieAsync(int id, string nieuweNaam)
        {
            var categorie = await GetCategorieByIdAsync(id);
            if (categorie != null)
            {
                categorie.Naam = nieuweNaam;
                await _context.SaveChangesAsync();
            }
        }
        // Get a category including Artikelen and Subcategorieën
        public async Task<Categorie?> GetCategorieMetRelatiesByIdAsync(int id)
        {
            return await _context.Categorieen
                .Include(c => c.Artikelen)
                .Include(c => c.Subcategorieën)
                .FirstOrDefaultAsync(c => c.CategorieId == id);
        }
        // Check if the category has subcategories
        public async Task<bool> HeeftSubCategorieenAsync(int id)
        {
            return await _context.Categorieen.AnyAsync(c => c.HoofdCategorieId == id);
        }
        // Check if the category has associated articles
        public async Task<bool> HeeftArtikelenAsync(int id)
        {
            return await _context.Categorieen
                .Where(c => c.CategorieId == id)
                .SelectMany(c => c.Artikelen)
                .AnyAsync();
        }
        // Delete a category only if it has no subcategories or articles
        public async Task<bool> DeleteCategorieAsync(int id)
        {
            var categorie = await GetCategorieMetRelatiesByIdAsync(id);

            if (categorie == null || categorie.Artikelen.Any() || categorie.Subcategorieën.Any())
            {
                return false; // Cannot delete non-empty category
            }

            _context.Categorieen.Remove(categorie);
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
