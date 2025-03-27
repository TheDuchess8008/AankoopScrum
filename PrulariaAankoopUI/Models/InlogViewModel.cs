using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models
{
    public class InlogViewModel
    {
        [Required(ErrorMessage = "Email is verplicht")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Paswoord is verplicht")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }
    }
}
