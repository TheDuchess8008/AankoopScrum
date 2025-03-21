using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrulariaAankoopData.Repositories;
public class SQLCategorieenRepository : ICategorieenRepository
{
    private readonly PrulariaComContext _context;

    public SQLCategorieenRepository(PrulariaComContext context)
    {
        _context = context;
    }

    public async Task<List<Categorie>> GetAlleCategorieenAsync()
    {
        return await _context.Categorieen.ToListAsync();
    }

    public async Task<Categorie> GetCategorieByIdAsync(int id)
    {
        return await _context.Categorieen.FindAsync(id);
    }
}
