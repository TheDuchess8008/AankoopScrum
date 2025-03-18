using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories;
public interface IActiecodesRepository
{
    Task<Actiecode> ToevoegActiecodeAsync(Actiecode actiecode);
    bool IsActiCodeNieuw(string naam, DateTime geldigVanDatum, DateTime geldigTotDatum);
}
