using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class InkomendeLeveringslijn
{
    public int InkomendeLeveringsId { get; set; }

    public int ArtikelId { get; set; }

    public int AantalGoedgekeurd { get; set; }

    public int AantalTeruggestuurd { get; set; }

    public int MagazijnPlaatsId { get; set; }

    public virtual Artikel Artikel { get; set; } = null!;

    public virtual InkomendeLevering InkomendeLevering { get; set; } = null!;

    public virtual Magazijnplaats MagazijnPlaats { get; set; } = null!;
}
