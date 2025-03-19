using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;
public class ActiecodesService
{
    private readonly IActiecodesRepository _actiecodesRepository;
    public ActiecodesService(IActiecodesRepository actiecodesRepository)
    {
        _actiecodesRepository = actiecodesRepository;
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
