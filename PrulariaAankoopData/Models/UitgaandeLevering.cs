using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class UitgaandeLevering
{
    public int UitgaandeLeveringsId { get; set; }

    public int BestelId { get; set; }

    public DateTime VertrekDatum { get; set; }

    public DateTime? AankomstDatum { get; set; }

    public string Trackingcode { get; set; } = null!;

    public int KlantId { get; set; }

    public int UitgaandeLeveringsStatusId { get; set; }

    public virtual Bestelling Bestel { get; set; } = null!;

    public virtual Klant Klant { get; set; } = null!;

    public virtual UitgaandeLeveringsStatus UitgaandeLeveringsStatus { get; set; } = null!;
}
