using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface IArtikelenRepository
{
    Task<List<Artikel>> GetArtikelenMetFilteren(int? categorieId, string? actiefStatus);
   // Task<Artikel> GetArtikelById(int id);
    Task<List<Categorie>> GetAlleCategorieen();
    Task AddArtikel(Artikel artikel);
    Task<Artikel?> GetArtikelById(int artikelId);
    Task UpdateArtikel(Artikel artikel);
}
