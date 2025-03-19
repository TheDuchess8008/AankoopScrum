using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public class SQLArtikelenRepository : IArtikelenRepository
{
    private readonly PrulariaComContext _context;
    public SQLArtikelenRepository(PrulariaComContext context)
    {
        this._context = context;
    }
    public async Task<Artikel> GetArtikelById(int id)
    {
        return await _context.Artikelen
                .Include(a => a.Leverancier)
                .Include(c => c.Categorieën)
                .FirstOrDefaultAsync(m => m.ArtikelId == id);
    }
    public async Task<List<Artikel>> GetListArtikelen(int categorieId)
    {
        if (categorieId == 0)
        {
            return await (_context.Artikelen
            .Include(c => c.Categorieën)
            .Include(l => l.Leverancier)
            .OrderBy(a => a.Naam)).ToListAsync();
        }
        else
        {
            return await (_context.Artikelen
            .Include(c => c.Categorieën)
            .Include(l => l.Leverancier)
            
            .OrderBy(a => a.Naam)).ToListAsync();
        }
    }
    public async Task<List<Categorie>> GetAlleCategorieen()
    {
        return await (_context.Categorieen).ToListAsync();
    }
}
