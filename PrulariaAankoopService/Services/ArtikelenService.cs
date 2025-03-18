using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    private readonly IArtikelenRepository artikelenRepository;
    private readonly PrulariaComContext _context;
    public ArtikelenService (IArtikelenRepository artikelenRepository, PrulariaComContext context)
    {
        this.artikelenRepository = artikelenRepository;
        _context = context;
    }

    public Artikel AddArtikel(Artikel artikel)
    {
        return artikelenRepository.Add(artikel);
    }
    public bool CheckOfArtikelBestaat(Artikel artikel)
    {
        
        var bestaandArtikel = _context.Artikelen.Where(a => a.Naam == artikel.Naam && a.Beschrijving == artikel.Beschrijving);
        if (bestaandArtikel is not null)
        {
            return true;
        }
        return false;
    }
}
