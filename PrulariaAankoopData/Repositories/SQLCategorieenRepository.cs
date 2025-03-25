using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public class SQLCategorieenRepository : ICategorieenRepository
{
    private readonly PrulariaComContext _context;
    public SQLCategorieenRepository(PrulariaComContext context)
    {
        _context = context;
    }
    public async Task HernoemCategorieAsync(int id, string nieuweNaam)
    {
        var categorie = await _context.Categorieen.FindAsync(id);
        if (categorie != null)
        {
            categorie.Naam = nieuweNaam;
            await _context.SaveChangesAsync();
        }
    }

}
