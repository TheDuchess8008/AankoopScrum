using Microsoft.AspNetCore.Mvc;

namespace PrulariaAankoopUI.Controllers;
public class SecurityController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Ingelogd") != null)
        {
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
}
