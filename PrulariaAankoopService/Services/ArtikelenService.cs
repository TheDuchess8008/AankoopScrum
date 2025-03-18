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
    private readonly IArtikelenRepository _sqlArtikelenRepository;
    public ArtikelenService(IArtikelenRepository sqlArtikelenRepository)
    {
        this._sqlArtikelenRepository = sqlArtikelenRepository;
    }

    public async Task<ArtikelViewModel> MaakGefilterdeLijstArtikelen(ArtikelViewModel form)
    {
        ArtikelViewModel filterLijst = new();
        form.Artikelen = await _sqlArtikelenRepository.GetAlleArtikelen();
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
}
