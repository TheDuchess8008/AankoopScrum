using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Personeelslid
{
    public int PersoneelslidId { get; set; }

    public string Voornaam { get; set; } = null!;

    public string Familienaam { get; set; } = null!;

    public bool? InDienst { get; set; }

    public int PersoneelslidAccountId { get; set; }

    public virtual ICollection<InkomendeLevering> InkomendeLeveringen { get; set; } = new List<InkomendeLevering>();

    public virtual Personeelslidaccount PersoneelslidAccount { get; set; } = null!;

    public virtual ICollection<SecurityGroep> SecurityGroepen { get; set; } = new List<SecurityGroep>();
}
