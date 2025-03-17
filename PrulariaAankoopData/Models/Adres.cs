using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Adres
{
    public int AdresId { get; set; }

    public string Straat { get; set; } = null!;

    public string HuisNummer { get; set; } = null!;

    public string? Bus { get; set; }

    public int PlaatsId { get; set; }

    public bool? Actief { get; set; }

    public virtual ICollection<Bestelling> BestellingenFacturatieAdres { get; set; } = new List<Bestelling>();

    public virtual ICollection<Bestelling> BestellingenLeveringsAdres { get; set; } = new List<Bestelling>();

    public virtual ICollection<Klant> KlantenFacturatieAdres { get; set; } = new List<Klant>();

    public virtual ICollection<Klant> KlantenLeveringsAdres { get; set; } = new List<Klant>();

    public virtual Plaats Plaats { get; set; } = null!;
}
