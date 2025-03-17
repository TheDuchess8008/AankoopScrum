using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Linq;

namespace PrulariaAankoopService.Services;

public class ArtikelenService
{
    private readonly PrulariaComContext _context;

    public ArtikelenService(PrulariaComContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Artikel GetArtikelByIdIfNotExists(int artikelId)
    {
        // Valideer het artikel ID (bijv. controleer of het een positief getal is)
        if (artikelId <= 0)
        {
            throw new ArgumentException("Ongeldig artikel ID. Het ID moet een positief getal zijn.", nameof(artikelId));
        }

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