using System.ComponentModel.DataAnnotations;
namespace PrulariaAankoopUI.Models;

public class ArtikelCategorieViewModel
{
    public int ArtikelId { get; set; }
    public string? ArtikelNaam { get; set; }
    public int CategorieId { get; set; }
    public string? CategorieNaam { get; set; }
}