using PrulariaAankoopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Repositories;
public interface ISecurityRepository
{
    Task<List<Personeelslid>> GetAllePersoneelsleden();
    Task<Personeelslid> GetGebruikerEnCheckEmail(string email);
}
