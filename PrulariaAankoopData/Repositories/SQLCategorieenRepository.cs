using Microsoft.EntityFrameworkCore;
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
        var categorie = await _context.Categorieen
            .Include(c => c.HoofdCategorie)
            .Where(c => c.CategorieId == id)
            .FirstOrDefaultAsync();
        return categorie;
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
    // Get a category including Artikelen and Subcategorieën
    public async Task<Categorie?> GetCategorieMetRelatiesByIdAsync(int id)
    {
        return await _context.Categorieen
            .Include(c => c.Artikelen)
            .Include(c => c.Subcategorieën)
            .FirstOrDefaultAsync(c => c.CategorieId == id);
    }
    // Check if the category has subcategories
    public async Task<bool> HeeftSubCategorieenAsync(int id)
    {
        return await _context.Categorieen.AnyAsync(c => c.HoofdCategorieId == id);
    }
    // Check if the category has associated articles
    public async Task<bool> HeeftArtikelenAsync(int id)
    {
        return await _context.Categorieen
            .Where(c => c.CategorieId == id)
            .SelectMany(c => c.Artikelen)
            .AnyAsync();
    }
    // Delete a category only if it has no subcategories or articles
    public async Task<bool> DeleteCategorieAsync(int id)
    {
        var categorie = await GetCategorieMetRelatiesByIdAsync(id);

        if (categorie == null || categorie.Artikelen.Any() || categorie.Subcategorieën.Any())
        {
            return false; // Cannot delete non-empty category
        }

        _context.Categorieen.Remove(categorie);
        await _context.SaveChangesAsync();
        return true;
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

    
    public async Task<bool> CategorieMetNaamAlBestaat(string categorieNaam)
    {
        return await _context.Categorieen
            .AnyAsync(c => c.Naam.ToLower() == categorieNaam.ToLower()); // case sensitivity included
    }

    public async Task CategorieToevoegen(Categorie categorie)
    {
        _context.Categorieen.Add(categorie);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Categorie>> GetHoofdCategorien()
    {
        return await _context.Categorieen.Where(c => c.HoofdCategorieId == null || c.Subcategorieën.Count > 0).ToListAsync();
    }


    // A.1500.Lesley
    // GetOverigeCategorieenAsync
    public async Task<List<Categorie>> GetOverigeCategorieen2Async(int categorieId)
    {
        // Haal alle categorieën inclusief hun subcategorieën en hoofdcategorieën op
        var lijstCategorieen = await _context.Categorieen
            .Include(c => c.Subcategorieën)
            .Include(c => c.HoofdCategorie)
            .ToListAsync();



        var lijstCategorieenZonderSubcategorieenEnZonderNietLegeHoofdcategorieen = lijstCategorieen
        .Where(c => c.HoofdCategorieId != categorieId)
        .Where(c => !(c.HoofdCategorieId == null && c.Subcategorieën.Any()))
        .ToList();


        return lijstCategorieenZonderSubcategorieenEnZonderNietLegeHoofdcategorieen;
    }


    // A.1500.Lesley
    // GetCategorieByIdMetHoofdEnSubcategorieenAsync
    public async Task<Categorie> GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(int id)
    {
        //return await _context.Categorieen.FindAsync(id);
        return await _context.Categorieen
        .Include(c => c.HoofdCategorie)
        .Include(a => a.Subcategorieën)
        .Include(b => b.Artikelen)
        .FirstOrDefaultAsync(m => m.CategorieId == id);
    }

    // A.1500.Lesley
    // SaveChangesAsync
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // A.1500.Lesley
    // HoofdcategorieIdOpNullZettenAsync
    public async Task HoofdcategorieIdOpNullZettenAsync(int categorieId)
    {
        var categorie = await _context.Categorieen.FirstOrDefaultAsync(c => c.CategorieId == categorieId);
        if (categorie != null)
        {
            categorie.HoofdCategorieId = null;
            await _context.SaveChangesAsync();
        }
    }


    // A.1300.Lesley
    // RemoveArtikelVanCategorieAsync
    public async Task<bool> RemoveArtikelVanCategorieAsync(Artikel artikel, Categorie categorie)
    {
        if (categorie.Artikelen.Remove(artikel))
        {
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }





}
