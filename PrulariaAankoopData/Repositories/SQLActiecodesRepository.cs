using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public class SQLActiecodesRepository : IActiecodesRepository
{
    private readonly PrulariaComContext _context;
    public SQLActiecodesRepository(PrulariaComContext context)
    {
        _context = context;
    }

    public async Task<Actiecode?> GetActiecodeByIdAsync(int id)
    {
        return await _context.Actiecodes.FindAsync(id);
    }

    public async Task<List<Actiecode>> GetAllActiecodesAsync()
    {
        return await _context.Actiecodes.ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}
