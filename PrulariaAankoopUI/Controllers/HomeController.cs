using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PrulariaAankoopData.Models;
using PrulariaAankoopService.Services;
using PrulariaAankoopUI.Models;

namespace PrulariaAankoopUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SecurityService _securityService;

        public HomeController(ILogger<HomeController> logger, SecurityService securityService)
        {
            _logger = logger;
            _securityService = securityService;
        }

        public IActionResult Index()
        {
            var naam = HttpContext.Session.GetString("Ingelogd");
            if (naam == null)
                ViewBag.Ingelogd = false;
            else ViewBag.Naam = naam;
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CheckInloggegevens(InlogViewModel inlogViewModel)
        {
            Personeelslid gebruiker = await _securityService.GetGebruikerEnCheckEmail(inlogViewModel.Email);
            if(await _securityService.CheckEmailEnPaswoord(gebruiker, inlogViewModel.Email, inlogViewModel.Wachtwoord) == false)
            {
                ModelState.AddModelError("Email", "Emailadres of paswoord is incorrect.");
            }
            else if(await _securityService.CheckSecuritygroep(gebruiker) == false)
            {
                ModelState.AddModelError("Email", "Dit account heeft geen toegang tot deze website.");
            }

            if (ModelState.IsValid)
            {
                var naam = gebruiker.Voornaam + " " + gebruiker.Familienaam;
                HttpContext.Session.SetString("Ingelogd", naam);
                return RedirectToAction("Index");
            }
            else ViewBag.Ingelogd = false;
            return View("Index");
        }

        public async Task<IActionResult> Uitloggen()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
