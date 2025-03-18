using System;
using System.Collections.Generic;
using System.Linq;
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

    public bool IsActiCodeNieuw(string naam, DateTime geldigVanDatum, DateTime geldigTotDatum) 
    {
        return _actiecodesRepository.IsActiCodeNieuw(naam, geldigVanDatum, geldigTotDatum);
    }

    public async Task RegistrerenActiecodeAsync(Actiecode actiecode) 
    {
        await _actiecodesRepository.ToevoegActiecodeAsync(actiecode);
    }
}
