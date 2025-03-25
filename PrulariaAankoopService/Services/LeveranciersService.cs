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
        private readonly ILeveranciersRepository _leveranciersRepository;

        public LeveranciersService(ILeveranciersRepository leveranciersRepository)
        {
            _leveranciersRepository = leveranciersRepository;
        }

        public async Task AddLeverancierAsync(Leverancier leverancier)
        {
            await _leveranciersRepository.AddLeverancierAsync(leverancier);
        }
    }
}
