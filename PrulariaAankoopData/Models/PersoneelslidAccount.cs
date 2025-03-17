using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Personeelslidaccount
{
    public int PersoneelslidAccountId { get; set; }

    public string Emailadres { get; set; } = null!;

    public string Paswoord { get; set; } = null!;

    public bool Disabled { get; set; }

    public virtual ICollection<Chatgespreklijn> Chatgespreklijnen { get; set; } = new List<Chatgespreklijn>();

    public virtual Personeelslid Personeelslid { get; set; }
}
