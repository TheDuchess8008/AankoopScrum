using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class NieuweLeverancierViewModel
{
    [Required]
    public string Naam { get; set; } = null!;

    [Required]
    [Display(Name = "Btw nummer")]
    public string BtwNummer { get; set; } = null!;

    [Required]
    public string Straat { get; set; } = null!;

    [Required]
    [Display(Name = "Huisnummer")]
    public string HuisNummer { get; set; } = null!;

    public string? Bus { get; set; }

    // Postcode dropdown
    [Display(Name = "Postcode")]
    public string? SelectedPostcode { get; set; }

    // Postcodes dropdown
    public List<SelectListItem>? Postcodes { get; set; }

    // Plaatsen dropdown (all places will be shown)
    public List<SelectListItem>? Plaatsen { get; set; }

    [Required]
    [Display(Name = "Plaats")]
    public int PlaatsId { get; set; }

    public string? Postcode { get; set; }

    [Required]
    [Display(Name = "Familienaam Van Contactpersoon")]
    public string FamilienaamContactpersoon { get; set; } = null!;

    [Required]
    [Display(Name = "Voornaam Van Contactpersoon")]
    public string VoornaamContactpersoon { get; set; } = null!;
}
