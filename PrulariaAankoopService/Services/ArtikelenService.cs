using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;
public class ArtikelenService
{
    private readonly IArtikelenRepository _artikelenRepository;
    public ArtikelenService(IArtikelenRepository sqlArtikelenRepository)
    {
        this._artikelenRepository = sqlArtikelenRepository;
    }

    public async Task<ArtikelViewModel> MaakGefilterdeLijstArtikelen(ArtikelViewModel form)
    {
        ArtikelViewModel filterLijst = new();
        form.Artikelen = await _artikelenRepository.GetListArtikelen(form.CategorieId);
        foreach (var artikel in form.Artikelen)
        {
            foreach (var categorie in artikel.Categorieën)
            {
                if (categorie.CategorieId == form.CategorieId || form.CategorieId == 0)
                {
                    if (artikel.MaximumVoorraad > 0 && form.ActiefStatus == "Actief" ||
                        artikel.MaximumVoorraad == 0 && form.ActiefStatus == "NonActief" ||
                        form.ActiefStatus == null)
                    {
                        filterLijst.Artikelen.Add(artikel);
                    }
                }
            }
        }
        return filterLijst;
    }
    public async Task<ArtikelViewModel> DetailsService(int id)
    {
        var artikelLijst = new ArtikelViewModel();
        artikelLijst.Artikel = await _artikelenRepository.GetArtikelById(id);
        artikelLijst.Categorieën = await _artikelenRepository.GetAlleCategorieen();
        return (artikelLijst);
    }
}
