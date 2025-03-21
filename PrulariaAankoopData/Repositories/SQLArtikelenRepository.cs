using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Linq;


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
    
    public async Task AddArtikel(Artikel artikel)
    {
        await _context.Artikelen.AddAsync(artikel);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateArtikel(Artikel? artikel)
    {
        if (artikel == null)
            throw new ArgumentNullException(nameof(artikel), "Artikel kan niet null zijn");
        var bestaandArtikel = await _context.Artikelen.FindAsync(artikel.ArtikelId);
        if (bestaandArtikel == null)
        {
            throw new Exception($"Artikel met ID {artikel.ArtikelId} werd niet gevonden.");
        }
        _context.Entry(bestaandArtikel).CurrentValues.SetValues(artikel);
        await _context.SaveChangesAsync();
    }
}










