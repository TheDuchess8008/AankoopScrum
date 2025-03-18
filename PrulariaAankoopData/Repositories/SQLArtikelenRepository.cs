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
    public async Task<List<Artikel>> GetAlleArtikelen()
    {
        return await (_context.Artikelen
            .Include(c => c.Categorieën)
            .Include(l => l.Leverancier)
            .OrderBy(a => a.Naam)).ToListAsync();
    }
}
