using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

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
        return await _actiecodesRepository.FindAsync(id.Value);
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







    public bool IsActiCodeNieuw(string naam, DateTime geldigVanDatum, DateTime geldigTotDatum) 
    {
        return _actiecodesRepository.IsActiCodeNieuw(naam, geldigVanDatum, geldigTotDatum);
    }

    public async Task RegistrerenActiecodeAsync(Actiecode actiecode) 
    {
        await _actiecodesRepository.ToevoegActiecodeAsync(actiecode);
    }


    public async Task<ActiecodeWijzigenViewModel?> GetActiecodeVoorWijzigingAsync(int id)
    {
        var actiecode = await _actiecodesRepository.GetActiecodeByIdAsync(id);
        if (actiecode == null) return null;

        return new ActiecodeWijzigenViewModel
        {
            ActiecodeId = actiecode.ActiecodeId,
            Naam = actiecode.Naam,
            GeldigVanDatum = actiecode.GeldigVanDatum,
            GeldigTotDatum = actiecode.GeldigTotDatum,
            IsEenmalig = actiecode.IsEenmalig
        };
    }

    public async Task<bool> WijzigActiecodeAsync(ActiecodeWijzigenViewModel model)
    {
        var actiecode = await _actiecodesRepository.GetActiecodeByIdAsync(model.ActiecodeId);
        if (actiecode == null) return false;

        if (actiecode.GeldigVanDatum < DateTime.Now) return false;
        if (model.GeldigTotDatum < actiecode.GeldigVanDatum || model.GeldigTotDatum < DateTime.Now) return false;

        actiecode.Naam = model.Naam;
        actiecode.GeldigTotDatum = model.GeldigTotDatum;
        actiecode.IsEenmalig = model.IsEenmalig;

        return await _actiecodesRepository.SaveChangesAsync();
    }

    public async Task<List<ActiecodeWijzigenViewModel>> GetAllActiecodesAsync()
    {
        var actiecodes = await _actiecodesRepository.GetAllActiecodesAsync();
        return actiecodes.Select(a => new ActiecodeWijzigenViewModel
        {
            ActiecodeId = a.ActiecodeId,
            Naam = a.Naam,
            GeldigVanDatum = a.GeldigVanDatum,
            GeldigTotDatum = a.GeldigTotDatum,
            IsEenmalig = a.IsEenmalig
        }).ToList();
    }
}
