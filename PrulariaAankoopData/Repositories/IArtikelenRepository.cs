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
    Task SaveChangesAsync();
    Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId);
    Task<bool> CategorieToevoegenAanArtikelAsync(Artikel artikel, Categorie categorie);




}
