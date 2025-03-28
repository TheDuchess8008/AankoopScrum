using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrulariaAankoopService.Services;
public class SecurityService
{
    private readonly ISecurityRepository _securityRepository;
    private readonly PrulariaComContext _context;
    public SecurityService(ISecurityRepository securityRepository, PrulariaComContext context)
    {
        _securityRepository = securityRepository;
        _context = context;
    }

    public async Task<Personeelslid> GetGebruikerEnCheckEmail(string email)
    {
        return await _securityRepository.GetGebruikerEnCheckEmail(email);
    }

    public async Task<bool> CheckEmailEnPaswoord(Personeelslid gebruiker, string email, string paswoord)
    {
        if(gebruiker is not null && paswoord is not null)
        {
            if (BCrypt.Net.BCrypt.Verify(paswoord, gebruiker.PersoneelslidAccount.Paswoord))
                return true;
        }
        return false;
    }

    public async Task<bool> CheckSecuritygroep(Personeelslid gebruiker)
    {
        foreach (var securitygroep in gebruiker.SecurityGroepen)
        {
            if (securitygroep.SecurityGroepId == 2)
            {
                return true;
            }
        }
        return false;
    }

    //wachtwoord wijzigen

    public async Task<Personeelslid?> GetIngelogdeLid(string voornaam, string familienaam) 
    {
        return await _securityRepository.GetIngelogdeLid(voornaam, familienaam);
    }

    public bool IsOudeWachtwoordJuist(Personeelslid gebruiker, string paswoord)
    {
        if (gebruiker is not null && paswoord is not null)
        {
            if (BCrypt.Net.BCrypt.Verify(paswoord, gebruiker.PersoneelslidAccount.Paswoord))
                return true;
        }
        return false;
    }

    public bool IsNieuwWachtwoordVerschillendVanOud(Personeelslid gebruiker, string nieuwePaswoord)
    {
        if (gebruiker is not null && nieuwePaswoord is not null)
        {
            if (BCrypt.Net.BCrypt.Verify(nieuwePaswoord, gebruiker.PersoneelslidAccount.Paswoord))
                return false;
        }
        return true;
    }

    public async Task<bool> WijzigWachtwoord(Personeelslid gebruiker, string nieuwPaswoord)
    {
        
        try
        {
            string nieuwGehashedPaswoord = BCrypt.Net.BCrypt.HashPassword(nieuwPaswoord);
            gebruiker.PersoneelslidAccount.Paswoord = nieuwGehashedPaswoord;
            _context.Personeelsleden.Update(gebruiker);
            await _context.SaveChangesAsync();
            return true; 
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
