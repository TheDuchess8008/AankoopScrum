using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Klant
{
    public int KlantId { get; set; }

    public int FacturatieAdresId { get; set; }

    public int LeveringsAdresId { get; set; }

    public virtual ICollection<Bestelling> Bestellingen { get; set; } = new List<Bestelling>();

    public virtual Adres FacturatieAdres { get; set; } = null!;

    public virtual Adres LeveringsAdres { get; set; } = null!;

    public virtual NatuurlijkePersoon? NatuurlijkePersoon { get; set; }

    public virtual RechtsPersoon? RechtsPersoon { get; set; }

    public virtual ICollection<UitgaandeLevering> UitgaandeLeveringen { get; set; } = new List<UitgaandeLevering>();
}
