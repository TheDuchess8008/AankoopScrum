using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Components;

public class EinddatumValidatieAttribute : ValidationAttribute
{
    private readonly string _begindatumProperty;

    public EinddatumValidatieAttribute(string begindatumProperty)
    {
        _begindatumProperty = begindatumProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var modelType = validationContext.ObjectInstance.GetType();
        var begindatumProperty = modelType.GetProperty(_begindatumProperty);
        if (begindatumProperty == null)
        {
            return new ValidationResult($"Onbekende eigenschap {_begindatumProperty}.");
        }

        var begindatumValue = begindatumProperty.GetValue(validationContext.ObjectInstance);
        if (value is DateTime einddatum && begindatumValue is DateTime begindatum)
        {
            if (einddatum <= begindatum)
            {
                return new ValidationResult("De einddatum moet later zijn dan de begindatum.");
            }
            if(einddatum < DateTime.Today)
            {
                return new ValidationResult("De einddatum moet vandaag of in de toekomst liggen.");
            }
        }

        return ValidationResult.Success;
    }
}