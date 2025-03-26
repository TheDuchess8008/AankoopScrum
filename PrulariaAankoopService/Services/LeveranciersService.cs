using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;
public class LeveranciersService
{

    private readonly ILeveranciersRepository _leveranciersRepository;

    public LeveranciersService(ILeveranciersRepository leveranciersRepository)
    {
        _leveranciersRepository = leveranciersRepository;
    }



    public async Task<Leverancier?> GetLeverancierByIdAsync(int id)
    {
        return await _leveranciersRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Plaats>> GetPlaatsenAsync()
    {
        return await _leveranciersRepository.GetAllPlaatsenAsync();
    }

    public async Task<bool> UpdateLeverancierAsync(Leverancier updatedLeverancier)
    {
        var leverancier = await _leveranciersRepository.GetByIdAsync(updatedLeverancier.LeveranciersId);
        if (leverancier == null)
        {
            return false;
        }

        leverancier.Naam = updatedLeverancier.Naam;
        leverancier.BtwNummer = updatedLeverancier.BtwNummer;
        leverancier.Straat = updatedLeverancier.Straat;
        leverancier.HuisNummer = updatedLeverancier.HuisNummer;
        leverancier.Bus = updatedLeverancier.Bus;
        leverancier.PlaatsId = updatedLeverancier.PlaatsId;
        leverancier.FamilienaamContactpersoon = updatedLeverancier.FamilienaamContactpersoon;
        leverancier.VoornaamContactpersoon = updatedLeverancier.VoornaamContactpersoon;

        return await _leveranciersRepository.SaveChangesAsync();
    }

}
