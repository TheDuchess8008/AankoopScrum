using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class NieuweLeverancierViewModel
{
    [Required(ErrorMessage = "De naam van de leverancier is verplicht.")]
    public string Naam { get; set; } = null!;

    [Required(ErrorMessage = "Het BTW-nummer is verplicht.")]
    [Display(Name = "Btw nummer")]
    public string BtwNummer { get; set; } = null!;

    [Required(ErrorMessage = "De straatnaam is verplicht.")]
    public string Straat { get; set; } = null!;

    [Required(ErrorMessage = "Het huisnummer is verplicht.")]
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

    [Required(ErrorMessage = "Een plaats is verplicht.")]
    [Display(Name = "Plaats")]
    public int PlaatsId { get; set; }

    public string? Postcode { get; set; }

    [Required(ErrorMessage = "De familienaam van de contactpersoon is verplicht.")]
    [Display(Name = "Familienaam Van Contactpersoon")]
    public string FamilienaamContactpersoon { get; set; } = null!;

    [Required(ErrorMessage = "De voornaam van de contactpersoon is verplicht.")]
    [Display(Name = "Voornaam Van Contactpersoon")]
    public string VoornaamContactpersoon { get; set; } = null!;
}
