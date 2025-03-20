using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface IArtikelenRepository
{
    Task<Artikel> GetArtikelById(int id);
    Task UpdateAsync(Artikel artikel);
}