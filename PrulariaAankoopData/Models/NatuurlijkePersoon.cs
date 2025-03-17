using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class NatuurlijkePersoon
{
    public int KlantId { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Familienaam { get; set; } = null!;

    public int GebruikersAccountId { get; set; }

    public virtual GebruikersAccount GebruikersAccount { get; set; } = null!;

    public virtual Klant Klant { get; set; } = null!;
}
