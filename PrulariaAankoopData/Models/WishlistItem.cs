using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class Wishlistitem
{
    public int WishListItemId { get; set; }

    public int ArtikelId { get; set; }

    public int GebruikersAccountId { get; set; }

    public DateTime AanvraagDatum { get; set; }

    public int Aantal { get; set; }

    public DateTime? EmailGestuurdDatum { get; set; }

    public virtual Artikel Artikel { get; set; } = null!;

    public virtual GebruikersAccount GebruikersAccount { get; set; } = null!;
}
