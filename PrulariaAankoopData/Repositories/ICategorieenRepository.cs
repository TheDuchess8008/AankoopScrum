using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface ICategorieenRepository
{
    Task<Categorie?> GetByIdAsync(int id);
    Task<List<Categorie>> GetSubcategorieenAsync(int hoofdCategorieId);
}
