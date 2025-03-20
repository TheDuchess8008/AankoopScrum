using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace PrulariaAankoopService.Services;
public class ArtikelenService
{
    private readonly IArtikelenRepository _repository;
    private readonly PrulariaComContext _context;
    public ArtikelenService(IArtikelenRepository repository, PrulariaComContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task UpdateArtikelAsync(Artikel artikel)
    {
        await _repository.UpdateAsync(artikel);
    }

    public async Task<Artikel> GetByIdAsync(int artikelId)
    {
        return await _repository.GetByIdAsync(artikelId);
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
}