using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;

namespace PrulariaAankoopService.Services;
public class ArtikelenService
{
    private readonly IArtikelenRepository _artikelenRepository;
    private readonly PrulariaComContext _context;
    public ArtikelenService(IArtikelenRepository artikelenRepository, PrulariaComContext context)
    {
        _artikelenRepository = artikelenRepository;
        _context = context;
    }

    public async Task UpdateArtikelAsync(Artikel artikel)
    {
        await _artikelenRepository.UpdateAsync(artikel);
    }

    public async Task<Artikel> GetByIdAsync(int artikelId)
    {
        return await _artikelenRepository.GetArtikelById(artikelId);
    }

    public async Task SetArtikelNonActiefAsync(int artikelId)
    {
        var artikel = await GetByIdAsync(artikelId);
        if (artikel == null)
            throw new ArgumentNullException(nameof(artikel), "Artikel kan niet null zijn");

        _context.Attach(artikel);

        //Velden op nul zetten
        artikel.MinimumVoorraad = 0;
        artikel.MaximumVoorraad = 0;
        artikel.Bestelpeil = 0;
        artikel.AantalBesteldLeverancier = 0;

        //ArtikelViewModel .ActiefStatus string op "NonActief"

        //Database updaten via UpdateArtikelAsync methode
        await UpdateArtikelAsync(artikel);
    }

    public async Task<ArtikelViewModel> MaakGefilterdeLijstArtikelen(ArtikelViewModel form)
    {
        ArtikelViewModel filterLijst = new();
        filterLijst.Artikelen = await _artikelenRepository.GetArtikelenMetFilteren(form.CategorieId, form.ActiefStatus);
        filterLijst.Categorieën = await _artikelenRepository.GetAlleCategorieen();
        return filterLijst;
    }
    public async Task<ArtikelViewModel> MaakDetailsArtikel(int id)
    {
        var artikelLijst = new ArtikelViewModel();
        artikelLijst.Artikel = await _artikelenRepository.GetArtikelById(id);
        var alleCategorieen = await _artikelenRepository.GetAlleCategorieen();
        foreach (var artikelCategorie in artikelLijst.Artikel.Categorieën)
        {
            foreach (var categorie in alleCategorieen)
            {
                if (artikelCategorie.HoofdCategorieId == categorie.CategorieId)
                {
                    if (!artikelLijst.Categorieën.Contains(categorie))
                        artikelLijst.Categorieën.Add(categorie);
                    break;
                }
            }
            artikelLijst.Categorieën.Add(artikelCategorie);
        }
        return (artikelLijst);
    }

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
    


    public async Task AddArtikel(Artikel artikel)
    {
        await _artikelenRepository.AddArtikel(artikel);
    }
    public bool CheckOfArtikelBestaat(Artikel artikel)
    {
        
        var bestaandArtikel = _context.Artikelen.Where(a => a.Naam == artikel.Naam && a.Beschrijving == artikel.Beschrijving)
            .FirstOrDefault();
        if (bestaandArtikel is not null)
        {
            return true;
        }
        return false;
    }
}
