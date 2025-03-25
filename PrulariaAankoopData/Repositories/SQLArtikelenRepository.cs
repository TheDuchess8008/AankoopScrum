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


}










