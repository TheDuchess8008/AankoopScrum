using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;
using System;
using System.Linq;

namespace PrulariaAankoopData.Repositories
{
    public class ArtikelRepository : IArtikelenRepository
    {
        private readonly PrulariaComContext _context;

        public ArtikelRepository(PrulariaComContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Artikel GetArtikelByIdIfNotExists(int artikelId)
        {
            // Controleer of het artikel al bestaat
            var bestaandArtikel = _context.Artikelen.FirstOrDefault(a => a.ArtikelId == artikelId);

            // Als het artikel niet bestaat, maak een nieuw artikel aan
            if (bestaandArtikel == null)
            {
                var nieuwArtikel = MaakNieuwArtikel(artikelId); // Maak een nieuw artikel aan
                _context.Artikelen.Add(nieuwArtikel);
                _context.SaveChanges();
                return nieuwArtikel;
            }

            // Als het artikel al bestaat, retourneer het
            return bestaandArtikel;
        }

        private Artikel MaakNieuwArtikel(int artikelId)
        {
            // Maak een nieuw artikel aan met standaardwaarden
            return new Artikel
            {
                ArtikelId = artikelId,
                Naam = "Nieuw Artikel",
                Prijs = 10.0m,
                Voorraad = 100
            };
        }
    }
}