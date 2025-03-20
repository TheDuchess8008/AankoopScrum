using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories;
public class SQLActiecodesRepository : IActiecodesRepository
{
    private readonly PrulariaComContext _context;
    public SQLActiecodesRepository(PrulariaComContext context)
    {
            _context = context;
    }



    // controleren of de Actiecode al bestaat
    public bool IsActiCodeNieuw(string naam, DateTime geldigVanDatum, DateTime geldigTotDatum)
    {
        var actiecode = _context.Actiecodes.FirstOrDefault(c=>
                c.Naam == naam &&
                c.GeldigVanDatum == geldigVanDatum &&
                c.GeldigTotDatum == geldigTotDatum
            );
        if ( actiecode == null ) 
            return true;
        else return false;
    }


    // Toevoeging van een nieuwe actiecode
    public async Task<Actiecode> ToevoegActiecodeAsync(Actiecode actiecode) 
    {
        await _context.AddAsync(actiecode);
        try 
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Actiecode werd intussen door een andere applicatie aangepast.");
        }
        return actiecode;
        
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

    //// SaveChangesAsync
    //public async Task<int> SaveChangesAsync()
    //{
    //    return await _context.SaveChangesAsync();
    //}

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
