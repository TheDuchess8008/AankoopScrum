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
        _context = context;
    }
    public async Task<Artikel> GetByIdAsync(int artikelId)
    {
        return await _context.Artikelen.FindAsync(artikelId);
    }
    public async Task UpdateAsync(Artikel artikel)
    {
        if (artikel == null)
            throw new ArgumentNullException(nameof(artikel), "Artikel kan niet null zijn");

        var existingArtikel = await GetByIdAsync(artikel.ArtikelId);
        if (existingArtikel == null)
            throw new KeyNotFoundException($"Artikel met ID {artikel.ArtikelId} niet gevonden.");

        _context.Entry(existingArtikel).CurrentValues.SetValues(artikel);

        await _context.SaveChangesAsync();
    }
}