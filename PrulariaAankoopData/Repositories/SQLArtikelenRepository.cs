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


    //-----------------------------------------------------------------------------------------------
    // A.800 Koen
    // GetArtikelById
    public async Task<Artikel> GetArtikelById(int id)
    {
        return await _context.Artikelen
                .Include(a => a.Leverancier)
                .Include(c => c.Categorieën)
                .FirstOrDefaultAsync(m => m.ArtikelId == id);
    }

    // A.800 Koen
    // GetArtikelenMetFilteren
    public async Task<List<Artikel>> GetArtikelenMetFilteren(int? categorieId, string? actiefStatus)
    {
        IQueryable<Artikel> query = _context.Artikelen
            .Include(c => c.Categorieën)
            .Include(l => l.Leverancier);
        if (categorieId == 1)
            query = query.Where(a => a.Categorieën.Any(c => c.CategorieId == categorieId || c.HoofdCategorieId == categorieId || c.HoofdCategorieId == 3));
        else if (categorieId != 0)
            query = query.Where(a => a.Categorieën.Any(c => c.CategorieId == categorieId || c.HoofdCategorieId == categorieId));

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

    // A.800 Koen
    // GetAlleCategorieen
    public async Task<List<Categorie>> GetAlleCategorieen()
    {
        return await (_context.Categorieen).ToListAsync();
    }
    //-----------------------------------------------------------------------------------------------
   


    // A.900 lesley 
    // GetArtikelMetCategorieenAsync
    public async Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId)
    {
        return await _context.Artikelen
            .Include(a => a.Categorieën)
            .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);
    }

    // A.900 lesley
    // IsCategorieLinkedToArtikelAsync
    public async Task<bool> IsCategorieLinkedToArtikelAsync(int artikelId, int categorieId)
    {
        var artikel = await _context.Artikelen
            .Include(a => a.Categorieën)
            .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);

        return artikel?.Categorieën.Any(c => c.CategorieId == categorieId) ?? false;
    }

    // A.900 lesley
    // AddCategorieAanArtikelAsync
    public async Task<bool> AddCategorieAanArtikelAsync(Artikel artikel, Categorie categorie)
    {
        artikel.Categorieën.Add(categorie);
        await _context.SaveChangesAsync();
        return true;
    }

    // A.900 Lesley
    // RemoveCategorieVanArtikelAsync
    public async Task<bool> RemoveCategorieVanArtikelAsync(Artikel artikel, Categorie categorie)
    {
        artikel.Categorieën.Remove(categorie);
        await _context.SaveChangesAsync();
        return true;
    }



    //-----------------------------------------------------------------------------------------------

    // merge
    // FindAsync
    public async Task<Artikel> GetByIdAsync(int artikelId)
    {
        return await _context.Artikelen.FindAsync(artikelId);
    }

    // merge
    // AddArtikel
    public async Task AddArtikel(Artikel artikel)
    {
        await _context.Artikelen.AddAsync(artikel);
        await _context.SaveChangesAsync();
    }

    // merge
    // UpdateArtikel
    public async Task UpdateArtikel(Artikel bestaandArtikel, Artikel artikel)
    {
        if (artikel == null)
            throw new ArgumentNullException(nameof(artikel), "Artikel kan niet null zijn");
        if (bestaandArtikel == null)
        {
            throw new Exception($"Artikel met ID {artikel.ArtikelId} werd niet gevonden.");
        }
        _context.Entry(bestaandArtikel).CurrentValues.SetValues(artikel);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Artikel>> GetArtikelsByCategorieIdAsync(int categorieId, string? zoekterm)
    {
        var artikels = await _context.Artikelen
            .Include(a => a.Categorieën)
            .ToListAsync();

        return artikels
            .Where(a => a.Categorieën.Any(c => c.CategorieId == categorieId))
            .Where(a => string.IsNullOrEmpty(zoekterm) ||
                        (!string.IsNullOrEmpty(a.Naam) &&
                         a.Naam.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)))
            .OrderBy(a => a.Naam)
            .ToList();
    }
    //Niet gelinkte artikels ophalen voor dropdown
    public async Task<List<Artikel>> GetNietGekoppeldeArtikelsVoorCategorieAsync(int categorieId)
    {
        var gekoppeldeArtikels = await _context.Categorieen
            .Where(c => c.CategorieId == categorieId)
            .SelectMany(c => c.Artikelen)
            .Select(a => a.ArtikelId)
            .ToListAsync();

        return await _context.Artikelen
            .Where(a => !gekoppeldeArtikels.Contains(a.ArtikelId))
            .OrderBy(a => a.Naam)
            .ToListAsync();
    }

    //returns true als geslaagd
    public async Task<bool> AddArtikelAanCategorieAsync(int artikelId, int categorieId)
    {
        var artikel = await _context.Artikelen
            .Include(a => a.Categorieën)
            .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);

        var categorie = await _context.Categorieen.FindAsync(categorieId);

        if (artikel == null || categorie == null) return false;

        if (artikel.Categorieën.Any(c => c.CategorieId == categorieId)) return false;

        artikel.Categorieën.Add(categorie);
        await _context.SaveChangesAsync();
        return true;
    }

}