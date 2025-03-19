using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories;
public interface IActiecodesRepository
{

    // ASYNCHROON ===========================================================
    Task<Actiecode?> FirstOrDefaultAsync(Expression<Func<Actiecode, bool>> predicate);
    Task<Actiecode?> FindAsync(int id);
    Task<int> SaveChangesAsync();
    Task<List<Actiecode>> ToListAsync();

    // SYNCHROON ===========================================================
    void Add(Actiecode actiecode);
    bool Any(Func<Actiecode, bool> predicate);
    void Remove(Actiecode actiecode);

    Task<Actiecode> ToevoegActiecodeAsync(Actiecode actiecode);
    bool IsActiCodeNieuw(string naam, DateTime geldigVanDatum, DateTime geldigTotDatum);
}
