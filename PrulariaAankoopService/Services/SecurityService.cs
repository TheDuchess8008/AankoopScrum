using Microsoft.AspNetCore.Http;
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
    public SecurityService(ISecurityRepository securityRepository)
    {
        _securityRepository = securityRepository;
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
}
