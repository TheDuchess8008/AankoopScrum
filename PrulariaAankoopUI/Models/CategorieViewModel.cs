using PrulariaAankoopData.Models;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models;

public class CategorieViewModel
{
    public int CategorieId { get; set; }

    [Display(Name = "Naam")]
    public string Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }

    [Display(Name = "Hoofdcategorie")]
    public virtual Categorie? HoofdCategorie { get; set; }

    public virtual ICollection<Categorie> Subcategorieën { get; set; } = new List<Categorie>();

    public virtual ICollection<Artikel> Artikelen { get; set; } = new List<Artikel>();
}
