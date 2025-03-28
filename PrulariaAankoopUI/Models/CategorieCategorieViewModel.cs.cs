using PrulariaAankoopData.Models;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models;

public class CategorieCategorieViewModel
{
    public Categorie Categorie { get; set; }
    public Categorie GekozenCategorie { get; set; }


    //*******************************************
    public int CategorieId { get; set; }
    [Display(Name = "Naam Van Categorie")]
    public string Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }
    [Display(Name = "Naam Van Hoofd Categorie")]

    public virtual Categorie? HoofdCategorie { get; set; }

    public virtual ICollection<Categorie> Subcategorieën { get; set; } = new List<Categorie>();

    public virtual ICollection<Artikel> Artikelen { get; set; } = new List<Artikel>();

}
