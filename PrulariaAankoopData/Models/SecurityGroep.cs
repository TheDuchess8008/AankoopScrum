using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class SecurityGroep
{
    public int SecurityGroepId { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<Personeelslid> Personeelsleden { get; set; } = new List<Personeelslid>();
}
