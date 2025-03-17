using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Chatgesprek
{
    public int ChatgesprekId { get; set; }

    public int GebruikersAccountId { get; set; }

    public virtual ICollection<Chatgespreklijn> Chatgespreklijnen { get; set; } = new List<Chatgespreklijn>();

    public virtual GebruikersAccount GebruikersAccount { get; set; } = null!;
}
