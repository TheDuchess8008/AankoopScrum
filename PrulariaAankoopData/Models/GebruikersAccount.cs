using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class GebruikersAccount
{
    public int GebruikersAccountId { get; set; }

    public string Emailadres { get; set; } = null!;

    public string Paswoord { get; set; } = null!;

    public bool Disabled { get; set; }

    public virtual ICollection<Chatgesprek> Chatgesprekken { get; set; } = new List<Chatgesprek>();

    public virtual ICollection<Chatgespreklijn> Chatgespreklijnen { get; set; } = new List<Chatgespreklijn>();

    public virtual ICollection<Contactpersoon> Contactpersonen { get; set; } = new List<Contactpersoon>();

    public virtual ICollection<NatuurlijkePersoon> Natuurlijkepersonen { get; set; } = new List<NatuurlijkePersoon>();

    public virtual ICollection<Wishlistitem> Wishlistitems { get; set; } = new List<Wishlistitem>();
}
