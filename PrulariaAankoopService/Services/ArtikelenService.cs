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
        /// Haalt een artikel op basis van ID.
        /// </summary>
        public async Task<Artikel?> GetArtikelById(int artikelId)
        {
            var artikel = await _artikelenRepository.GetArtikelById(artikelId);
            if (artikel == null)
            {
                throw new Exception($"Artikel met ID {artikelId} werd niet gevonden.");
            }
            return artikel;
        }

        /// <summary>
        /// Valideert en wijzigt een artikel.
        /// </summary>
        public async Task UpdateArtikel(Artikel artikel)
        {
            if (artikel == null)
            {
                throw new ArgumentNullException(nameof(artikel), "Artikel mag niet null zijn.");
            }

            if (string.IsNullOrWhiteSpace(artikel.Naam))
            {
                throw new Exception("De naam van het artikel mag niet leeg zijn.");
            }

            if (artikel.Prijs <= 0)
            {
                throw new Exception("De prijs moet een positief getal zijn.");
            }

            // Controleer of het artikel al bestaat
            var bestaandArtikel = await _artikelenRepository.GetArtikelById(artikel.ArtikelId);
            if (bestaandArtikel == null)
            {
                throw new Exception($"Artikel met ID {artikel.ArtikelId} werd niet gevonden.");
            }

            // Update de waarden
            bestaandArtikel.Naam = artikel.Naam;
            bestaandArtikel.Beschrijving = artikel.Beschrijving;
            bestaandArtikel.Prijs = artikel.Prijs;
            bestaandArtikel.Voorraad = artikel.Voorraad;
            bestaandArtikel.LeveranciersId = artikel.LeveranciersId;

            // Sla de wijzigingen op in de database
            await _artikelenRepository.UpdateArtikel(bestaandArtikel);
        }
    }
}

