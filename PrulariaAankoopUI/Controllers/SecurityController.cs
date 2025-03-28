using Microsoft.AspNetCore.Mvc;
using PrulariaAankoopData.Models;
using PrulariaAankoopData.Repositories;
using PrulariaAankoopService.Services;
using PrulariaAankoopUI.Models;

namespace PrulariaAankoopUI.Controllers;
public class SecurityController : Controller
{
    
    private readonly SecurityService _securityService;
    
    public SecurityController(SecurityService securityService)
    {
        _securityService = securityService;
    }
   
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Ingelogd") != null)
        {
            var naam = HttpContext.Session.GetString("Ingelogd");
            if (naam == null)
                ViewBag.Ingelogd = false;
            else ViewBag.Naam = naam;
            return View();

        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> WachtWoordWijzigen() 
    {
        if (HttpContext.Session.GetString("Ingelogd") != null)
        {

            return View();

        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> WachtWoordWijzigen(WachtwoordWijzigenViewModel model) 
    {
        if (HttpContext.Session.GetString("Ingelogd") != null)
        {

            var personeelslid = new Personeelslid();

            // de naam en familienaam uit de session variabele ophalen:
            string ingelogd = HttpContext.Session.GetString("Ingelogd");
            var naamParts = ingelogd.Split(' ');
            string ingelogdeNaam = naamParts[0];
            string ingelogdeFamilienaam = naamParts[1];

            personeelslid = await _securityService.GetIngelogdeLid(ingelogdeNaam, ingelogdeFamilienaam);
            if (ModelState.IsValid) 
            {
                if (_securityService.IsOudeWachtwoordJuist(personeelslid, model.OudeWachtwoord))
                {
                    if (_securityService.IsNieuwWachtwoordVerschillendVanOud(personeelslid, model.NieuweWachtwoord)) 
                    {
                        await _securityService.WijzigWachtwoord(personeelslid, model.NieuweWachtwoord);
                     
                        TempData["WachtwoordGewijzigd"] = "Wachtwoord werd sucessvol gewijzigd.";
                        return RedirectToAction("Index");
                        
                    }
                    else ViewBag.foutBerincht = "Het nieuwe wachtwoord mag niet hetzelfde zijn als het oude!";
                }
                else ViewBag.foutBerincht = "Het ingevoerde oude wachtwoord is niet correct!";
            }
            return View(model);
        }
        
        return RedirectToAction("Index", "Home");
    }


}
