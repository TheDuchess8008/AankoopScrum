using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrulariaAankoopUI.Models;

public class CategorieToevoegenViewModel
{
    [Required(ErrorMessage = "Naam is verplicht.")]
    [Display(Name = "Naam van Categorie")]
    [StringLength(30, ErrorMessage = "Naam moet minstens 3 en maximaal 30 tekens bevatten.", MinimumLength = 3)]
    public string Naam { get; set; }
    
    [Display(Name = "Naam van Hoofd Categorie")]
    public int? HoofdCategorieId { get; set; }
    
    public SelectList? HoofdCategorien { get; set; }
}