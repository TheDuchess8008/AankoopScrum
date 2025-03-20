using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

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
    public async Task<List<Artikel>> GetArtikelenMetFilteren(int? categorieId, string? actiefStatus)
    {
        IQueryable<Artikel> query = _context.Artikelen.Include(c => c.Categorieën)
            .Include(l => l.Leverancier);
        if (categorieId != 0)
            query = query.Where(a => a.Categorieën.Any(c => c.CategorieId == categorieId));
        if (actiefStatus == "Actief")
        {
            query = query.Where(a => a.MaximumVoorraad > 0);
        }
        if (actiefStatus == "NonActief")
        {
            query = query.Where(a => a.MaximumVoorraad == 0);
        }
        var gefilterdeLijstArtikelen = await query.OrderBy(a => a.Naam).ToListAsync();
        return gefilterdeLijstArtikelen;
    }
    public async Task<List<Categorie>> GetAlleCategorieen()
    {
        return await (_context.Categorieen).ToListAsync();
    }

    public Artikel Add(Artikel artikel)
    {
        _context.Artikelen.Add(artikel);
        _context.SaveChangesAsync();
        return artikel;
    }
}
