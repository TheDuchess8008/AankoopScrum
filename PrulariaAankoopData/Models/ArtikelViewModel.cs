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

    public string Naam { get; set; } = null!;

    public string Beschrijving { get; set; } = null!;

    public decimal Prijs { get; set; }
    [Display(Name = "Gewicht In Gram")]

    public int GewichtInGram { get; set; }

    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }
    [Display(Name = "Minimum Voorraad")]

    public int MinimumVoorraad { get; set; }
    [Display(Name = "Maximum Voorraad")]

    public int MaximumVoorraad { get; set; }

    public int Levertijd { get; set; }
    [Display(Name = "Aantal Besteld Leverancier")]

    public int AantalBesteldLeverancier { get; set; }
    [Display(Name = "Max Aantal In Magazijn Plaats")]

    public int MaxAantalInMagazijnPlaats { get; set; }
    [Display(Name = "Leveranciers Id")]

    public int LeveranciersId { get; set; }

    // Related data
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
