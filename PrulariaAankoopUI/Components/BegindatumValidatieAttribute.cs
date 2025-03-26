using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Components;

public class BegindatumValidatieAttribute : ValidationAttribute
{
    private readonly string _isEditProperty;
    private readonly string _origineleBegindatumProperty;

    public BegindatumValidatieAttribute(string isEditProperty, string origineleBegindatumProperty)
    {
        _isEditProperty = isEditProperty;
        _origineleBegindatumProperty = origineleBegindatumProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var modelType = validationContext.ObjectInstance.GetType();

        // Ophalen van IsEdit en OrigineleBegindatum
        var isEditProperty = modelType.GetProperty(_isEditProperty);
        var origineleBegindatumProperty = modelType.GetProperty(_origineleBegindatumProperty);

        if (isEditProperty == null || origineleBegindatumProperty == null)
        {
            return new ValidationResult($"Interne fout: {_isEditProperty} of {_origineleBegindatumProperty} niet gevonden.");
        }

        var isEdit = (bool)isEditProperty.GetValue(validationContext.ObjectInstance);
        var origineleBegindatum = (DateTime)origineleBegindatumProperty.GetValue(validationContext.ObjectInstance);

        if (value is DateTime begindatum)
        {
            // Bij een nieuwe actiecode mag de begindatum niet in het verleden liggen
            if (!isEdit && begindatum < DateTime.Today)
            {
                return new ValidationResult("De begindatum moet vandaag of later zijn.");
            }

            // Bij een bestaande actiecode: wijziging is alleen toegestaan als begindatum nog niet gestart is
            if (isEdit && begindatum != origineleBegindatum && begindatum < DateTime.Today)
            {
                return new ValidationResult("De begindatum mag niet worden aangepast naar een datum eerder dan vandaag.");
            }
        }

        return ValidationResult.Success;
    }
}
