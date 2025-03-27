using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface ICategorieenRepository
{
    Task<Categorie?> GetCategorieByIdAsync(int id);
    Task<IEnumerable<Categorie>> GetAllCategorieenAsync();
    Task HernoemCategorieAsync(int categorieId, string nieuweNaam);
    Task<bool> HeeftSubCategorieenAsync(int categorieId);
    Task<bool> HeeftArtikelenAsync(int categorieId);
    Task<bool> DeleteCategorieAsync(int id);
    Task<Categorie?> GetCategorieMetRelatiesByIdAsync(int id); // New method for related data
}
