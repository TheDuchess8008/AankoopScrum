using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopData.Models;

public class NieuweActiecodeViewModel 
{
    [Required(ErrorMessage = "De naam van de actiecode is verplicht.")]
    public string Naam { get; set; } = null!;

    [Required(ErrorMessage = "De begindatum is verplicht.")]
    [DataType(DataType.Date)]
    [Display(Name = "Geldig vanaf")]
    [CustomValidation(typeof(NieuweActiecodeViewModel), nameof(ValidateGeldigVanDatum))]
    public DateTime GeldigVanDatum { get; set; }

    [Required(ErrorMessage = "De einddatum is verplicht.")]
    [DataType(DataType.Date)]
    [Display(Name = "Geldig tot")]
    [CustomValidation(typeof(NieuweActiecodeViewModel), nameof(ValidateGeldigTotDatum))]
    public DateTime GeldigTotDatum { get; set; }

    public bool IsEenmalig { get; set; }

    public static ValidationResult ValidateGeldigVanDatum(DateTime geldigVanDatum, ValidationContext context)
    {
        if (geldigVanDatum < DateTime.Now.Date)
        {
            return new ValidationResult("De begindatum moet vandaag of in de toekomst liggen.");
        }
        return ValidationResult.Success;
    }

    public static ValidationResult ValidateGeldigTotDatum(DateTime geldigTotDatum, ValidationContext context)
    {
        var model = (NieuweActiecodeViewModel)context.ObjectInstance;
        if (geldigTotDatum < model.GeldigVanDatum)
        {
            return new ValidationResult("De einddatum moet na de begindatum liggen.");
        }
        return ValidationResult.Success;
    }
}
