using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Chatgespreklijn
{
    public int ChatgesprekLijnId { get; set; }

    public int ChatgesprekId { get; set; }

    public string Bericht { get; set; } = null!;

    public DateTime Tijdstip { get; set; }

    public int? GebruikersAccountId { get; set; }

    public int? PersoneelslidAccountId { get; set; }

    public virtual Chatgesprek Chatgesprek { get; set; } = null!;

    public virtual GebruikersAccount? GebruikersAccount { get; set; }

    public virtual Personeelslidaccount? PersoneelslidAccount { get; set; }
}
