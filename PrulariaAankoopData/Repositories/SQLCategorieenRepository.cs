using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
