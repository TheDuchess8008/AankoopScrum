using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories
{
    public class SQLArtikelenRepository : IArtikelenRepository
    {
        private readonly PrulariaComContext _context;

        public SQLArtikelenRepository(PrulariaComContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
                
    }
}
