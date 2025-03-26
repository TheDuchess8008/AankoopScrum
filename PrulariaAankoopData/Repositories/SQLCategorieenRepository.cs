using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories;

public class SQLCategorieenRepository : ICategorieenRepository
{
    private readonly PrulariaComContext _context;

    public SQLCategorieenRepository(PrulariaComContext context)
    {
        _context = context;
    }

    public async Task<Categorie?> GetByIdAsync(int id)
    {
        return await _context.Categorieen
            .Include(c => c.Subcategorieën)
                .ThenInclude(sub => sub.Artikelen)
            .Include(c => c.Artikelen)
            .Include(c => c.HoofdCategorie)
            .FirstOrDefaultAsync(c => c.CategorieId == id);
    }


    public async Task<List<Categorie>> GetSubcategorieenAsync(int hoofdCategorieId)
    {
        return await _context.Categorieen
            .Where(c => c.HoofdCategorieId == hoofdCategorieId)
            .Include(c => c.Artikelen)
            .ToListAsync();
    }
}