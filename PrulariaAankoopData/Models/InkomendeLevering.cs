using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class InkomendeLevering
{
    public int InkomendeLeveringsId { get; set; }

    public int LeveranciersId { get; set; }

    public string LeveringsbonNummer { get; set; } = null!;

    public DateTime Leveringsbondatum { get; set; }

    public DateTime LeverDatum { get; set; }

    public int OntvangerPersoneelslidId { get; set; }

    public virtual ICollection<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; } = new List<InkomendeLeveringslijn>();

    public virtual Leverancier Leverancier { get; set; } = null!;

    public virtual Personeelslid OntvangerPersoneelslid { get; set; } = null!;
}
