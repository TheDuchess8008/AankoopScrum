using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories;
public interface ICategorieenRepository
{
    Task<List<Categorie>> GetLijstCategorieen();
    Task<List<Categorie>> GetAlleCategorieenAsync();
    Task<Categorie> GetCategorieByIdAsync(int id);
    Task<List<Categorie?>> GetOverigeCategorieenAsync(int artikelId);    
    Task<IEnumerable<Categorie>> GetAllCategorieenAsync();
    Task HernoemCategorieAsync(int categorieId, string nieuweNaam);
    Task<bool> HeeftSubCategorieenAsync(int categorieId);
    Task<bool> HeeftArtikelenAsync(int categorieId);
    Task<bool> DeleteCategorieAsync(int id);
    Task<Categorie?> GetCategorieMetRelatiesByIdAsync(int id); // New method for related data
    Task<Categorie?> GetByIdAsync(int id);
    Task<List<Categorie>> GetSubcategorieenAsync(int hoofdCategorieId);
    Task<bool> CategorieMetNaamAlBestaat(string categorieNaam);

    Task CategorieToevoegen(Categorie categorie);
    Task<List<Categorie>> GetHoofdCategorien();




    //Task<List<Categorie>> GetLijstCategorieen();
    //Task<List<Categorie>> GetAlleCategorieenAsync();
    //Task<Categorie> GetCategorieByIdAsync(int id); // gebruikt in A.1300.Lesley.Test
    Task<List<Categorie>> GetOverigeCategorieen2Async(int categorieId); // A.1500.Lesley
    Task<Categorie> GetCategorieByIdMetHoofdEnSubcategorieenEnArtikelenAsync(int id);// A.1500.Lesley
    Task<int> SaveChangesAsync(); // A.1500.Lesley
    Task HoofdcategorieIdOpNullZettenAsync(int categorieId); // A.1500.Lesley
    //Task<bool> RemoveArtikelVanCategorieAsync(int artikelId, Categorie categorie);
    Task<bool> RemoveArtikelVanCategorieAsync(Artikel artikel, Categorie categorie); // A.1300.Lesley

   
}
