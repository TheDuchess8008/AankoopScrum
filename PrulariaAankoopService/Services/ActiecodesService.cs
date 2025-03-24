using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;
public class ActiecodesService
{


    private IActiecodesRepository _actiecodesRepository;
    public ActiecodesService(IActiecodesRepository actiecodesRepository)
    {
        _actiecodesRepository = actiecodesRepository;
    }

    // ASYNCHROON ===========================================================

    // FindAsync !
    public async Task<Actiecode?> FindAsync(int? id)
    {
        if (id == null) return null;
        return await _actiecodesRepository.FindByIdAsync(id.Value);
    }

    // FirstOrDefaultAsync
    public async Task<Actiecode?> FirstOrDefaultAsync(Expression<Func<Actiecode, bool>> predicate)
    {
        return await _actiecodesRepository.FirstOrDefaultAsync(predicate);
    }

    // SaveChangesAsync
    public async Task<bool> SaveChangesAsync()
    {
        return await _actiecodesRepository.SaveChangesAsync();
    }

    // ToListAsync
    public async Task<List<Actiecode>> ToListAsync()
    {
        return await _actiecodesRepository.ToListAsync();
    }

    // SYNCHROON ===========================================================

    // Add
    public void Add(Actiecode actiecode)
    {
        _actiecodesRepository.Add(actiecode);
    }

    // Any
    public bool Any(Func<Actiecode, bool> predicate)
    {
        return _actiecodesRepository.Any(predicate);
    }

    // Remove
    public void Remove(Actiecode actiecode)
    {
        _actiecodesRepository.Remove(actiecode);
    }

    public bool IsActieCodeNieuw(string naam, DateTime geldigVanDatum, DateTime geldigTotDatum) 
    {
        return _actiecodesRepository.IsActieCodeNieuw(naam, geldigVanDatum, geldigTotDatum);
    }

    public async Task RegistrerenActiecodeAsync(Actiecode actiecode) 
    {
        await _actiecodesRepository.ToevoegActiecodeAsync(actiecode);
    }

    public async Task UpdateAsync(Actiecode actiecode) 
    {
        await _actiecodesRepository.UpdateAsync(actiecode);
    }
}
