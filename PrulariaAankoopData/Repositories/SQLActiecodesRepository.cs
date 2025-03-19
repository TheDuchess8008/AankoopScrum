using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    // ASYNCHROON ===========================================================

    // FindAsync
    public async Task<Actiecode?> FindAsync(int id)
    {
        return await _context.Actiecodes.FindAsync(id);
    }

    // FirstOrDefaultAsync
    public async Task<Actiecode?> FirstOrDefaultAsync(Expression<Func<Actiecode, bool>> predicate)
    {
        return await _context.Actiecodes.FirstOrDefaultAsync(predicate);
    }

    // SaveChangesAsync
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // ToListAsync
    public async Task<List<Actiecode>> ToListAsync()
    {
        return await _context.Actiecodes.ToListAsync();
    }

    // SYNCHROON ===========================================================

    // Add
    public void Add(Actiecode actiecode)
    {
        _context.Actiecodes.Add(actiecode);
    }

    // Any

    public bool Any(Func<Actiecode, bool> predicate)
    {
        return _context.Actiecodes.Any(predicate);
    }

    // Remove
    public void Remove(Actiecode actiecode)
    {
        _context.Actiecodes.Remove(actiecode);
    }

}

