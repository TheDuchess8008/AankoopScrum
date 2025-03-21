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


    //-----------------------------------------------------------------------------------------------
    // KOEN
    // GetArtikelById
    public async Task<Artikel> GetArtikelById(int id)
    {
        return await _context.Artikelen
                .Include(a => a.Leverancier)
                .Include(c => c.Categorieën)
                .FirstOrDefaultAsync(m => m.ArtikelId == id);
    }

    // KOEN
    // GetArtikelenMetFilteren
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

    // KOEN
    // GetAlleCategorieen
    public async Task<List<Categorie>> GetAlleCategorieen()
    {
        return await (_context.Categorieen).ToListAsync();
    }
    //-----------------------------------------------------------------------------------------------
    // NIEUW

 

    // GetArtikelMetCategorieenAsync
    public async Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId)
    {
        return await _context.Artikelen
            .Include(a => a.Categorieën)
            .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);
    }


    // IsCategorieLinkedToArtikelAsync
    public async Task<bool> IsCategorieLinkedToArtikelAsync(int artikelId, int categorieId)
    {
        var artikel = await _context.Artikelen
            .Include(a => a.Categorieën)
            .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);

        return artikel?.Categorieën.Any(c => c.CategorieId == categorieId) ?? false;
    }

    // AddCategorieAanArtikelAsync
    public async Task<bool> AddCategorieAanArtikelAsync(Artikel artikel, Categorie categorie)
    {
        artikel.Categorieën.Add(categorie);
        await _context.SaveChangesAsync();
        return true;
    }






}

