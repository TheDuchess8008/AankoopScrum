using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface IArtikelenRepository
{
    // KOEN
    Task<Artikel> GetArtikelById(int id);
    Task<List<Artikel>> GetArtikelenMetFilteren(int? categorieId, string? actiefStatus);
    Task<List<Categorie>> GetAlleCategorieen();

    //-----------------------------------------------------------------------------------------------
    // NIEUW
    Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId);
    Task<bool> IsCategorieLinkedToArtikelAsync(int artikelId, int categorieId);
    Task<bool> AddCategorieAanArtikelAsync(Artikel artikel, Categorie categorie);


}
