using PrulariaAankoopData.Models;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models
{
    public class LeverancierWijzigenViewModel
    {

        public int LeveranciersId { get; set; }

        [Required(ErrorMessage = "De naam van de leverancier is verplicht.")]
        public string Naam { get; set; } = null!;

        [Required(ErrorMessage = "Het BTW-nummer is verplicht.")]
        [Display(Name = "Btw nummer")]
        public string BtwNummer { get; set; } = null!;

        [Required(ErrorMessage = "De straatnaam is verplicht.")]
        public string Straat { get; set; } = null!;

        [Required(ErrorMessage = "Het huisnummer is verplicht.")]
        [Display(Name = "Huisnr")]
        public string HuisNummer { get; set; } = null!;

        public string? Bus { get; set; }

        [Required(ErrorMessage = "Een plaats is verplicht.")]
        public int PlaatsId { get; set; }

        [Required(ErrorMessage = "De familienaam van de contactpersoon is verplicht.")]
        [Display(Name = "Familienaam Van Contactpersoon")]
        public string FamilienaamContactpersoon { get; set; } = null!;

        [Required(ErrorMessage = "De voornaam van de contactpersoon is verplicht.")]
        [Display(Name = "Voornaam Van Contactpersoon")]
        public string VoornaamContactpersoon { get; set; } = null!;

        public virtual ICollection<Artikel> Artikelen { get; set; } = new List<Artikel>();

        public virtual ICollection<InkomendeLevering> InkomendeLeveringen { get; set; } = new List<InkomendeLevering>();

        public virtual Plaats? Plaats { get; set; } = null!;


    }
}
