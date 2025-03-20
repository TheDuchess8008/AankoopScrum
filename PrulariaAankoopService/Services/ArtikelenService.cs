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
        foreach (var categorie in artikelLijst.Artikel.Categorieën)
        {
            if (!artikelLijst.Categorieën.Contains(alleCategorieen[(int)categorie.HoofdCategorieId - 1]))
                artikelLijst.Categorieën.Add(alleCategorieen[(int)categorie.HoofdCategorieId - 1]);
            artikelLijst.Categorieën.Add(categorie);
        }
        return (artikelLijst);
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
