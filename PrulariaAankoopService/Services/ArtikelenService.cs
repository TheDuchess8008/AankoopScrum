using System;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopService.Services
{
    public class ArtikelenService
    {
        private readonly IArtikelenRepository _artikelenRepository;

        public ArtikelenService(IArtikelenRepository artikelenRepository)
        {
            _artikelenRepository = artikelenRepository ?? throw new ArgumentNullException(nameof(artikelenRepository));
        }

    }
}
