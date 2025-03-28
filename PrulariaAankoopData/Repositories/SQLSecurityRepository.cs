using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public class SQLSecurityRepository : ISecurityRepository
{
    private readonly PrulariaComContext _context;
    public SQLSecurityRepository(PrulariaComContext context)
    {
        _context = context;
    }
    public async Task<List<Personeelslid>> GetAllePersoneelsleden()
    {
        return await _context.Personeelsleden
                    .Include(a => a.SecurityGroepen)
                    .Include(m => m.PersoneelslidAccount)
                    .ToListAsync();
    }

    public async Task<Personeelslid> GetGebruikerEnCheckEmail(string email)
    {
        List<Personeelslid> personeelsleden = await GetAllePersoneelsleden();
        //Check email
        return personeelsleden
                .Where(a => a.PersoneelslidAccount.Emailadres == email)
                .FirstOrDefault();
    }

    public async Task<Personeelslid?> GetIngelogdeLid(string voornaam, string familienaam) 
    {
        return await _context.Personeelsleden
               .Include(a => a.SecurityGroepen)
               .Include(m => m.PersoneelslidAccount)
               .Where(p => p.Voornaam == voornaam && p.Familienaam == familienaam)
               .FirstOrDefaultAsync();
    }
}
