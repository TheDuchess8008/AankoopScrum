using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models;
public class CategorieArtikelViewModel
{
    public int CategorieId { get; set; }
    public string CategorieNaam { get; set; } = string.Empty;

    [Display(Name = "Artikel")]
    [Required(ErrorMessage = "Selecteer een artikel.")]
    public int ArtikelId { get; set; }

    public List<SelectListItem> BeschikbareArtikelen { get; set; } = new();
}
