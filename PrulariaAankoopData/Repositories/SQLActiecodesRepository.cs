using System;
using System.Collections.Generic;
using System.Linq;
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
}
