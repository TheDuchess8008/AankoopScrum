using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrulariaAankoopData.Repositories;
public class SQLCategorieenRepository : ICategorieenRepository
{
    private readonly PrulariaComContext _context;
    public SQLCategorieenRepository(PrulariaComContext context)
    {
        _context = context;
    }
    public async Task<List<Categorie>> GetLijstCategorieen()
    {
        var lijstCategorieen = await (_context.Categorieen
                .Include(c => c.HoofdCategorie)
                .Include(c => c.Subcategorieën)
                .Include(a => a.Artikelen)
                .OrderBy(c => c.Naam)).ToListAsync();
        return lijstCategorieen;
    }
   

    public async Task<List<Categorie>> GetAlleCategorieenAsync()
    {
        return await _context.Categorieen.ToListAsync();
    }

    public async Task<Categorie> GetCategorieByIdAsync(int id)
    {
        return await _context.Categorieen.FindAsync(id);
    }

    // get list of Categorieen, not linked to an artikel with a given id, and that this categories have a parent category
    public async Task<List<Categorie>> GetOverigeCategorieenAsync(int artikelId)
    {
        var gekoppeldeCategorieen = await _context.Artikelen
            .Where(a => a.ArtikelId == artikelId) 
            .SelectMany(a => a.Categorieën)
            .Select(c => c.CategorieId)
            .ToListAsync();
        
// categories that are NOT in gekoppeldeCategorien
        return await _context.Categorieen
            .Where(c => !gekoppeldeCategorieen.Contains(c.CategorieId) && c.HoofdCategorieId != null)
            .ToListAsync();
    }

}
