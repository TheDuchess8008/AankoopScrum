using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories
{
    public class SQLArtikelenRepository : IArtikelenRepository
    {
        private readonly PrulariaComContext _context;

        public SQLArtikelenRepository(PrulariaComContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // SaveChangesAsync
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // GetArtikelMetCategorieenAsync
        public async Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId)
        {
            return await _context.Artikelen
                .Include(a => a.Categorieën)
                .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);
        }

        // CategorieToevoegenAanArtikelAsync
        public async Task<bool> CategorieToevoegenAanArtikelAsync(Artikel artikel, Categorie categorie)
        {
            artikel.Categorieën.Add(categorie);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
