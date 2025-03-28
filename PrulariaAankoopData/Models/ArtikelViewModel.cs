using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Models;


public class ArtikelViewModel
{
    public int ArtikelId { get; set; }

    public string Ean { get; set; } = null!;
    [Required(ErrorMessage = "De naam is verplicht.")]
    public string Naam { get; set; } = null!;
    [Required(ErrorMessage = "Beschrijving is verplicht.")]
    public string Beschrijving { get; set; } = null!;
    [Required(ErrorMessage = "Prijs is verplicht.")]
    public decimal Prijs { get; set; }
    [Display(Name = "Gewicht In Gram")]
    [Required(ErrorMessage = "Gewicht is verplicht.")]
    public int GewichtInGram { get; set; }
    [Required(ErrorMessage = "Bestelpijl is verplicht.")]
    public int Bestelpeil { get; set; }
 
    public int Voorraad { get; set; }
    [Display(Name = "Minimum Voorraad")]
    [Required(ErrorMessage = "Minimum voorraad is verplicht.")]
    public int MinimumVoorraad { get; set; }
    [Display(Name = "Maximum Voorraad")]
[Required(ErrorMessage = "Maximum voorraad is verplicht.")]
    public int MaximumVoorraad { get; set; }
    [Required(ErrorMessage = "Levertijd is verplicht.")]
    public int Levertijd { get; set; }
    [Display(Name = "Aantal Besteld Leverancier")]
    [Required(ErrorMessage = "Aantal besteld is verplicht.")]
    public int AantalBesteldLeverancier { get; set; }
    [Display(Name = "Max Aantal In Magazijn Plaats")]
    [Required(ErrorMessage = "Aantal in magazijn is verplicht.")]
    public int MaxAantalInMagazijnPlaats { get; set; }
    [Display(Name = "Leveranciers Id")]

    public int LeveranciersId { get; set; }

    // Related data
    [Required(ErrorMessage = "Leveranciernaam is verplicht.")]
    public string LeverancierNaam { get; set; } = null!;
    public Artikel Artikel { get; set; }

    public virtual Leverancier Leverancier { get; set; } = null!;

    // Confirmation message (if applicable)
    public bool IsUpdateSuccessful { get; set; }
    public string ConfirmationMessage { get; set; } = null!;

    public virtual List<Categorie> Categorieën { get; set; } = new List<Categorie>();
    public List<Artikel> Artikelen { get; set; } = new List<Artikel>();
    public string ActiefStatus { get; set; }
    public int CategorieId { get; set; }
}
