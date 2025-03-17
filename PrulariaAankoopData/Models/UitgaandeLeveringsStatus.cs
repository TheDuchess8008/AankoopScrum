using System;
using System.Collections.Generic;

namespace PrulariaAankoopData.Models;

public class UitgaandeLeveringsStatus
{
    public int UitgaandeLeveringsStatusId { get; set; }

    public string Naam { get; set; } = null!;

    public virtual ICollection<UitgaandeLevering> UitgaandeLeveringen { get; set; } = new List<UitgaandeLevering>();
}
