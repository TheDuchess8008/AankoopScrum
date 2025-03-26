using Microsoft.AspNetCore.Mvc;

namespace PrulariaAankoopUI.Components
{
    public class IngelogdViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string naam = HttpContext.Session.GetString("Ingelogd");
            if (naam != null)
                ViewBag.Naam = naam;
            return View();
        }
    }
}
