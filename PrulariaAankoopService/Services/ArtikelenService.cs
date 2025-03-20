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

        // GetArtikelMetCategorieenAsync
        public async Task<Artikel> GetArtikelMetCategorieenAsync(int artikelId)
        {
            return await _artikelenRepository.GetArtikelMetCategorieenAsync(artikelId);
        }

        // CategorieToevoegenAanArtikelAsync
        public async Task<bool> CategorieToevoegenAanArtikelAsync(int artikelId, Categorie categorie)
        {
            var artikel = await _artikelenRepository.GetArtikelMetCategorieenAsync(artikelId);
            if (artikel == null)
                throw new ArgumentException("Artikel niet gevonden.");

            if (categorie == null)
                throw new ArgumentException("Categorie is ongeldig.");

            if (artikel.Categorieën.Any(c => c.CategorieId == categorie.CategorieId))
                throw new InvalidOperationException("Categorie is al gekoppeld aan het artikel.");

            return await _artikelenRepository.CategorieToevoegenAanArtikelAsync(artikel, categorie);
        }





    }
}
