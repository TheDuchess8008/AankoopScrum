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
}
