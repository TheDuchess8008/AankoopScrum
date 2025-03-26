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
        private readonly PrulariaComContext _context;

        public LeveranciersService(ILeveranciersRepository leveranciersRepository, PrulariaComContext context)
        {
            _leveranciersRepository = leveranciersRepository;
            _context = context;
        }

        public async Task<List<Leverancier>> GetAllLeveranciersAsync()
        {
            return await _leveranciersRepository.GetAllLeveranciersAsync();
        }

        public async Task AddLeverancierAsync(Leverancier leverancier)
        {
            await _leveranciersRepository.AddLeverancierAsync(leverancier);
            await _context.SaveChangesAsync(); // Hier wordt SaveChangesAsync nu vanuit de service aangeroepen
        }
    }
}
