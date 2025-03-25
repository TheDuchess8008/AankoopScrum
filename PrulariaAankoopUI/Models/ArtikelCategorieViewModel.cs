using System.ComponentModel.DataAnnotations;
namespace PrulariaAankoopUI.Models;

public class ArtikelCategorieViewModel
{
    public int ArtikelId { get; set; }
    public string? ArtikelNaam { get; set; }
    [Required(ErrorMessage = "Selecteer een categorie.")]
    public int CategorieId { get; set; }
    [Required(ErrorMessage = "Selecteer een categorie.")]
    public string? CategorieNaam { get; set; }
}