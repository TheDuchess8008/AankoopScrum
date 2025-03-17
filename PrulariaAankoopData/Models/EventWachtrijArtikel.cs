using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public partial class EventWachtrijArtikel
{
    public int ArtikelId { get; set; }

    public int Aantal { get; set; }

    public int MaxAantalInMagazijnPlaats { get; set; }
}
