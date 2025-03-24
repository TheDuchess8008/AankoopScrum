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

    public async Task UpdateArtikelNonActief(Artikel artikel)
    {
        var bestaandArtikel = await _artikelenRepository.GetArtikelById(artikel.ArtikelId);
        await _artikelenRepository.UpdateArtikel(bestaandArtikel, artikel);
    }

    public async Task<Artikel> GetByIdAsync(int artikelId)
    {
        return await _artikelenRepository.GetArtikelById(artikelId);
    }

    public async Task SetArtikelNonActiefAsync(int artikelId)
    {
        var artikel = await _artikelenRepository.GetByIdAsync(artikelId);
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
        await UpdateArtikelNonActief(artikel);
    }

    //-----------------------------------------------------------

    // KOEN
    // MaakGefilterdeLijstArtikelen
    public async Task<ArtikelViewModel> MaakGefilterdeLijstArtikelen(ArtikelViewModel form)
    {
        ArtikelViewModel filterLijst = new();
        filterLijst.Artikelen = await _artikelenRepository.GetArtikelenMetFilteren(form.CategorieId, form.ActiefStatus);
        filterLijst.Categorieën = await _artikelenRepository.GetAlleCategorieen();
        return filterLijst;
    }

    // KOEN
    // MaakDetailsArtikel
    public async Task<ArtikelViewModel> MaakDetailsArtikel(int id)
    {
        var artikel = new ArtikelViewModel();
        artikel.Artikel = await _artikelenRepository.GetArtikelById(id);
        var alleCategorieen = await _artikelenRepository.GetAlleCategorieen();
        foreach (var artikelCategorie in artikel.Artikel.Categorieën)
        {
            foreach (var categorie in alleCategorieen)
            {
                if (artikelCategorie.HoofdCategorieId == categorie.CategorieId)
                {
                    if (!artikel.Categorieën.Contains(categorie))
                        artikel.Categorieën.Add(categorie);
                    break;
                }
            }
            artikel.Categorieën.Add(artikelCategorie);
        }
        return (artikel);
    }

    /// Haalt een artikel op basis van ID.
    /// </summary>
    public async Task<Artikel?> GetArtikelById(int artikelId)
    {
        Artikel artikel = await _artikelenRepository.GetArtikelById(artikelId);
        if (artikel == null)
        {
            throw new Exception($"Artikel met ID {artikelId} werd niet gevonden.");
        }
        return artikel;
    }

    /// <summary>
    /// Valideert en wijzigt een artikel.
    /// </summary>
    public async Task UpdateArtikel(ArtikelViewModel artikelViewModel)
    {
        if (artikelViewModel == null)
        {
            throw new ArgumentNullException(nameof(artikelViewModel.Artikel), "Artikel mag niet null zijn.");
        }

        if (string.IsNullOrWhiteSpace(artikelViewModel.Artikel.Naam))
        {
            throw new Exception("De naam van het artikel mag niet leeg zijn.");
        }

        if (artikelViewModel.Artikel.Prijs <= 0)
        {
            throw new Exception("De prijs moet een positief getal zijn.");
        }

        // Controleer of het artikel al bestaat
        var bestaandArtikel = await _artikelenRepository.GetArtikelById(artikelViewModel.Artikel.ArtikelId);
        if (bestaandArtikel == null)
        {
            throw new Exception($"Artikel met ID {artikelViewModel.Artikel.ArtikelId} werd niet gevonden.");
        }
        Artikel artikel = bestaandArtikel;
        artikel.ArtikelId = artikelViewModel.Artikel.ArtikelId;
        artikel.AantalBesteldLeverancier = artikelViewModel.Artikel.AantalBesteldLeverancier;
        artikel.Ean = artikelViewModel.Artikel.Ean;
        artikel.Naam = artikelViewModel.Artikel.Naam;
        artikel.Beschrijving = artikelViewModel.Artikel.Beschrijving;
        artikel.Prijs = artikelViewModel.Artikel.Prijs;
        artikel.GewichtInGram = artikelViewModel.Artikel.GewichtInGram;
        artikel.Bestelpeil = artikelViewModel.Artikel.Bestelpeil;
        artikel.Voorraad = artikelViewModel.Artikel.Voorraad;
        artikel.MinimumVoorraad = artikelViewModel.Artikel.MinimumVoorraad;
        artikel.MaximumVoorraad = artikelViewModel.Artikel.MaximumVoorraad;
        artikel.Levertijd = artikelViewModel.Artikel.Levertijd;
        artikel.AantalBesteldLeverancier = artikelViewModel.Artikel.AantalBesteldLeverancier;
        artikel.MaxAantalInMagazijnPlaats = artikelViewModel.Artikel.MaxAantalInMagazijnPlaats;
        artikel.LeveranciersId = artikelViewModel.Artikel.LeveranciersId;
        
        // Sla de wijzigingen op in de database
        await _artikelenRepository.UpdateArtikel(bestaandArtikel, artikel);
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


    //-----------------------------------------------------------
    // NIEUW


    public async Task<bool> IsCategorieLinkedToArtikelAsync(int artikelId, int categorieId)
    {
        return await _artikelenRepository.IsCategorieLinkedToArtikelAsync(artikelId, categorieId);
    }


    public async Task<bool> AddCategorieAanArtikelAsync(int artikelId, Categorie categorie)
    {
        var artikel = await _artikelenRepository.GetArtikelMetCategorieenAsync(artikelId);
        if (artikel == null)
            throw new ArgumentException("Artikel niet gevonden.");

        if (categorie == null)
            throw new ArgumentException("Categorie is ongeldig.");

        
        var isLinked = await _artikelenRepository.IsCategorieLinkedToArtikelAsync(artikelId, categorie.CategorieId);
        if (isLinked)
            throw new InvalidOperationException("Categorie is al gekoppeld aan het artikel.");

        return await _artikelenRepository.AddCategorieAanArtikelAsync(artikel, categorie);
    }



}
