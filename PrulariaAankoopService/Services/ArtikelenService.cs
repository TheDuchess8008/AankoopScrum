using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;

namespace PrulariaAankoopService.Services;
public class ArtikelenService
{
    private readonly IArtikelenRepository _artikelenRepository;
    public ArtikelenService(IArtikelenRepository artikelenRepository)
    {
        this._artikelenRepository = artikelenRepository;
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
        var artikelLijst = new ArtikelViewModel();
        artikelLijst.Artikel = await _artikelenRepository.GetArtikelById(id);
        var alleCategorieen = await _artikelenRepository.GetAlleCategorieen(); // artikelLijst = ArtikelViewModel object
        foreach (var artikelCategorie in artikelLijst.Artikel.Categorieën) // artikelLijst = ArtikelViewModel object
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
