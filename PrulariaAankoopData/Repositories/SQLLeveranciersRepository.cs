using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public class SQLLeveranciersRepository : ILeverancierRepository
{private readonly PrulariaComContext _context;

    public SQLLeveranciersRepository(PrulariaComContext context)
    {
        _context = context;
    }

    public async Task<List<Leverancier>> GetAllLeveranciersAsync()
    {
        return await _context.Leveranciers
            .Include(l => l.Plaats)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task AddLeverancierAsync(Leverancier leverancier)
    {
        _context.Leveranciers.Add(leverancier);
        await _context.SaveChangesAsync();
    }
}

