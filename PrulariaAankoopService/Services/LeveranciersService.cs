using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PrulariaAankoopService.Services
{
    public class LeveranciersService
    {
        private readonly ILeverancierRepository _leverancierRepository;

        public LeveranciersService(ILeverancierRepository leverancierRepository)
        {
            _leverancierRepository = leverancierRepository;
        }

        public async Task AddLeverancierAsync(Leverancier leverancier)
        {
            await _leverancierRepository.AddLeverancierAsync(leverancier);
        }
    }
}
