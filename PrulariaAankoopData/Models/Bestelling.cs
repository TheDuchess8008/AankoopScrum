using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Bestelling
{
    public int BestelId { get; set; }

    public DateTime Besteldatum { get; set; }

    public int KlantId { get; set; }

    public bool Betaald { get; set; }

    public string? Betalingscode { get; set; }

    public int BetaalwijzeId { get; set; }

    public bool Annulatie { get; set; }

    public DateTime? Annulatiedatum { get; set; }

    public string? Terugbetalingscode { get; set; }

    public int BestellingsStatusId { get; set; }

    public bool ActiecodeGebruikt { get; set; }

    public string? Bedrijfsnaam { get; set; }

    public string? BtwNummer { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Familienaam { get; set; } = null!;

    public int FacturatieAdresId { get; set; }

    public int LeveringsAdresId { get; set; }

    public virtual ICollection<Bestellijn> Bestellijnen { get; set; } = new List<Bestellijn>();

    public virtual BestellingsStatus BestellingsStatus { get; set; } = null!;

    public virtual Betaalwijze Betaalwijze { get; set; } = null!;

    public virtual Adres FacturatieAdres { get; set; } = null!;

    public virtual Klant Klant { get; set; } = null!;

    public virtual Adres LeveringsAdres { get; set; } = null!;

    public virtual ICollection<UitgaandeLevering> Uitgaandeleveringen { get; set; } = new List<UitgaandeLevering>();
}
