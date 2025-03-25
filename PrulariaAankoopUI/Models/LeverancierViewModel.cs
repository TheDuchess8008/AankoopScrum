using PrulariaAankoopData.Models;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models;

public class LeverancierViewModel
{
    public int LeveranciersId { get; set; }

    public string Naam { get; set; } = null!;
    [Display(Name = "Btw nummer")]
    public string BtwNummer { get; set; } = null!;

    public string Straat { get; set; } = null!;
    [Display(Name = "Huisnr")]
    public string HuisNummer { get; set; } = null!;

    public string? Bus { get; set; }

    public int PlaatsId { get; set; }
    [Display(Name = "Familienaam Van Contactpersoon")]

    public string FamilienaamContactpersoon { get; set; } = null!;
    [Display(Name = "Voornaam Van Contactpersoon")]

    public string VoornaamContactpersoon { get; set; } = null!;

    public virtual ICollection<Artikel> Artikelen { get; set; } = new List<Artikel>();

    public virtual Plaats? Plaats { get; set; } = null!;
}
