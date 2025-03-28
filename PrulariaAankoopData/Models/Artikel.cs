using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopData.Models;

public class Artikel
{
    public int ArtikelId { get; set; }

    public string Ean { get; set; } = null!;
    [Required(ErrorMessage = "De naam is verplicht.")]
    public string Naam { get; set; } = null!;
    [Required(ErrorMessage = "Beschrijving is verplicht.")]
    public string Beschrijving { get; set; } = null!;
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Prijs is verplicht.")]
    public decimal Prijs { get; set; }
    [Required(ErrorMessage = "Gewicht is verplicht.")]
    public int GewichtInGram { get; set; }
    [Required(ErrorMessage = "Bestelpijl is verplicht.")]
    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }
    [Required(ErrorMessage = "Minimum voorraad is verplicht.")]
    public int MinimumVoorraad { get; set; }
    [Required(ErrorMessage = "Maximum voorraad is verplicht.")]
    public int MaximumVoorraad { get; set; }
    [Required(ErrorMessage = "Levertijd is verplicht.")]
    public int Levertijd { get; set; }
    [Required(ErrorMessage = "Aantal besteld is verplicht.")]
    public int AantalBesteldLeverancier { get; set; }
    [Required(ErrorMessage = "Maximum aantal in magazijn is verplicht.")]
    public int MaxAantalInMagazijnPlaats { get; set; }

    public int LeveranciersId { get; set; }

    public virtual ICollection<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; } = new List<ArtikelLeveranciersInfolijn>();

    public virtual ICollection<Bestellijn> Bestellijnen { get; set; } = new List<Bestellijn>();

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual Leverancier? Leverancier { get; set; } = null!;

    public virtual ICollection<Magazijnplaats> Magazijnplaatsen { get; set; } = new List<Magazijnplaats>();

    public virtual ICollection<Veelgesteldevragenartikel> Veelgesteldevragenartikel { get; set; } = new List<Veelgesteldevragenartikel>();

    public virtual ICollection<Wishlistitem> Wishlistitems { get; set; } = new List<Wishlistitem>();

    public virtual List<Categorie> Categorieën { get; set; } = new List<Categorie>();
}
