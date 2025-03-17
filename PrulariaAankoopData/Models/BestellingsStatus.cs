using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class BestellingsStatus
{
    public int BestellingsStatusId { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<Bestelling> Bestellingen { get; set; } = new List<Bestelling>();
}
