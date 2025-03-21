using PrulariaAankoopData.Models;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Components;

public class VanTotDateValidatie
{
    // Valideer of de begindatum (GeldigVanDatum) vandaag of in de toekomst is
    public static ValidationResult ValidateGeldigVanDatum(DateTime geldigVanDatum, ValidationContext context)
    {
        if (geldigVanDatum < DateTime.Now.Date)
        {
            return new ValidationResult("De begindatum moet vandaag of in de toekomst liggen.");
        }
        return ValidationResult.Success;
    }

    // Valideer of de einddatum (GeldigTotDatum) na de begindatum (GeldigVanDatum) ligt
    public static ValidationResult ValidateGeldigTotDatum(DateTime geldigTotDatum, ValidationContext context)
    {
        var model = context.ObjectInstance;
        var startDateProperty = model.GetType().GetProperty("GeldigVanDatum");

        if (startDateProperty != null)
        {
            DateTime geldigVanDatum = (DateTime)startDateProperty.GetValue(model);

            if (geldigTotDatum < geldigVanDatum)
            {
                return new ValidationResult("De einddatum moet na de begindatum liggen.");
            }
        }
        else
        {
            return new ValidationResult("De begindatum kon niet worden gevonden.");
        }

        return ValidationResult.Success;
    }
}
