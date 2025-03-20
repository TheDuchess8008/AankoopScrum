using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Artikel
{
    public int ArtikelId { get; set; }

    public string Ean { get; set; } = null!;

    public string Naam { get; set; } = null!;

    public string Beschrijving { get; set; } = null!;

    public decimal Prijs { get; set; }

    public int GewichtInGram { get; set; }

    public int Bestelpeil { get; set; }

    public int Voorraad { get; set; }

    public int MinimumVoorraad { get; set; }

    public int MaximumVoorraad { get; set; }

    public int Levertijd { get; set; }

    public int AantalBesteldLeverancier { get; set; }

    public int MaxAantalInMagazijnPlaats { get; set; }

    public int LeveranciersId { get; set; }

    public virtual ICollection<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; } = new List<ArtikelLeveranciersInfolijn>();

    public virtual ICollection<Bestellijn> Bestellijnen { get; set; } = new List<Bestellijn>();

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual Leverancier? Leverancier { get; set; } = null!;

    public virtual ICollection<Magazijnplaats> Magazijnplaatsen { get; set; } = new List<Magazijnplaats>();

    public virtual ICollection<Veelgesteldevragenartikel> Veelgesteldevragenartikel { get; set; } = new List<Veelgesteldevragenartikel>();

    public virtual ICollection<Wishlistitem> Wishlistitems { get; set; } = new List<Wishlistitem>();

    public virtual ICollection<Categorie> Categorieën { get; set; } = new List<Categorie>();
}
