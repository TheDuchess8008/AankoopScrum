using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopData.Models;

public class Actiecode
{
    public int ActiecodeId { get; set; }

    public string Naam { get; set; } = null!;

    [Display(Name = "Geldig Van Datum")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime GeldigVanDatum { get; set; }

    [Display(Name = "Geldig Tot Datum")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime GeldigTotDatum { get; set; }

    [Display(Name = "Is Eenmalig")]
    public bool IsEenmalig { get; set; }
}
