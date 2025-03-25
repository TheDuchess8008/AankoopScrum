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
    
    //Task<Artikel> GetArtikelById(int id);
    //Task<List<Artikel>> GetArtikelenMetFilteren(int? categorieId, string? actiefStatus);
    Task<List<Categorie>> GetAlleCategorieen();
    Task AddArtikel(Artikel artikel);
    Task<Artikel?> GetArtikelById(int artikelId);
    Task<Artikel> GetByIdAsync(int artikelId);
    Task UpdateArtikel(Artikel bestaandArtikel, Artikel artikel);

    //-----------------------------------------------------------------------------------------------
    // A.900 lesley
    Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId);
    Task<bool> IsCategorieLinkedToArtikelAsync(int artikelId, int categorieId);
    Task<bool> AddCategorieAanArtikelAsync(Artikel artikel, Categorie categorie);

    //-----------------------------------------------------------------------------------------------
    // A.800 lesley (oplossing probleem , geen artikelen na filteren op hoofdcategorie)

    Task<List<Artikel>> GetArtikelenMetFilteren(int? categorieId, string? actiefStatus);
    Task<bool> RemoveCategorieVanArtikelAsync(Artikel artikel, Categorie categorie);
}
