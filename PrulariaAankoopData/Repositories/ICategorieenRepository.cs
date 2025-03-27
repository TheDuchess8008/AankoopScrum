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
    Task<List<Categorie>> GetOverigeCategorieenAsync(int artikelId);
}
