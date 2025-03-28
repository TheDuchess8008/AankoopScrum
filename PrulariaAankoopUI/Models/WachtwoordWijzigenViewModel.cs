using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models;

public class WachtwoordWijzigenViewModel
{
    [Required(ErrorMessage = "Oude wachtwoord is verplicht")]
    [DataType(DataType.Password)]
    public string OudeWachtwoord { get; set; }

    [Required(ErrorMessage = "Voer een nieuw wachtwoord in")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Het nieuwe wachtwoord moet minimaal 8 tekens bevatten, een hoofdletter, een cijfer en een speciaal teken.")]
    public string NieuweWachtwoord { get; set; }

    [Required(ErrorMessage = "Bevestig het nieuwe wachtwoord.")]
    [DataType(DataType.Password)]
    [Compare("NieuweWachtwoord", ErrorMessage = "De nieuwe wachtwoorden komen niet overeen.")]
    public string HerhaaldeNieuweWachtwoord { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
}
