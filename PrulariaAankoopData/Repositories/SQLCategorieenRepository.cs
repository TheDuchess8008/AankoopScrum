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
    }
}
