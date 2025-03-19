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

        public async Task<Artikel> DetailsService(int artikelId)
        {

            var artikel = await _artikelenRepository.GetArtikelById(artikelId);
            if (artikel == null)
            {
                throw new Exception($"Artikel met ID {artikelId} werd niet gevonden.");
            }
            return artikel;
        }
    }
}
