using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class RechtsPersoon
{
    public int KlantId { get; set; }

    public string Naam { get; set; } = null!;

    public string BtwNummer { get; set; } = null!;

    public virtual ICollection<Contactpersoon> Contactpersonen { get; set; } = new List<Contactpersoon>();

    public virtual Klant Klant { get; set; } = null!;
}
