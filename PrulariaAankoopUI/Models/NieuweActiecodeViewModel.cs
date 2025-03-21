using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using PrulariaAankoopUI.Components;

namespace PrulariaAankoopData.Models;

public class NieuweActiecodeViewModel 
{
    [Required(ErrorMessage = "De naam van de actiecode is verplicht.")]
    public string Naam { get; set; } = null!;

    [Required(ErrorMessage = "De begindatum is verplicht.")]
    [Display(Name = "Geldig vanaf")]
    [CustomValidation(typeof(VanTotDateValidatie), nameof(VanTotDateValidatie.ValidateGeldigVanDatum))]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime GeldigVanDatum { get; set; }

    [Required(ErrorMessage = "De einddatum is verplicht.")]
    [Display(Name = "Geldig tot")]
    [CustomValidation(typeof(VanTotDateValidatie), nameof(VanTotDateValidatie.ValidateGeldigTotDatum))]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime GeldigTotDatum { get; set; }

    public bool IsEenmalig { get; set; }

}
