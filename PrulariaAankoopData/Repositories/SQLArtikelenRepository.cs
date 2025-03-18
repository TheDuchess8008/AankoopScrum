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
    public Artikel Add(Artikel artikel)
    {
        _context.Artikelen.Add(artikel);
        _context.SaveChangesAsync();
        return artikel;
    }
}
