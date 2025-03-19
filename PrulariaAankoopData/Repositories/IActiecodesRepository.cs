using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface IActiecodesRepository
{
    Task<Actiecode?> GetActiecodeByIdAsync(int id);
    Task<List<Actiecode>> GetAllActiecodesAsync();
    Task<bool> SaveChangesAsync();
}
