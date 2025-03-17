using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public partial class Magazijnplaats
{
    public int MagazijnPlaatsId { get; set; }

    public int? ArtikelId { get; set; }

    public string Rij { get; set; } = null!;

    public int Rek { get; set; }

    public int Aantal { get; set; }

    public virtual Artikel? Artikel { get; set; }

    public virtual ICollection<InkomendeLeveringslijn> InkomendeLeveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();
}
