using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface ILeveranciersRepository
{



    Task<Leverancier?> GetByIdAsync(int id);
    Task<IEnumerable<Plaats>> GetAllPlaatsenAsync();
    Task<bool> SaveChangesAsync();

    Task<List<Leverancier>> GetAllLeveranciersAsync();
    Task AddLeverancierAsync(Leverancier leverancier);
}

