using System;
using System.Threading.Tasks;
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

        public async Task<Artikel?> GetArtikelById(int artikelId)
        {
            return await _context.Artikelen
                .Include(a => a.Leverancier) // Inclusief gerelateerde leverancier
                .FirstOrDefaultAsync(a => a.ArtikelId == artikelId);
        }

        public async Task UpdateArtikel(Artikel artikel)
        {
            _context.Artikelen.Update(artikel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateArtikel(Artikel? artikel)
        {
            var bestaandArtikel = await _context.Artikelen.FindAsync(artikel.ArtikelId);

            if (bestaandArtikel == null)
            {
                throw new Exception($"Artikel met ID {artikel.ArtikelId} werd niet gevonden.");
            }

            _context.Entry(bestaandArtikel).CurrentValues.SetValues(artikel);
            await _context.SaveChangesAsync();
        }
    }
}
