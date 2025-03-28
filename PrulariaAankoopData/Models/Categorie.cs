using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopData.Models;

public class Categorie
{
    public int CategorieId { get; set; }
  
    [Display(Name = "Naam Van Categorie")]
    public string Naam { get; set; } = null!;
   
    public int? HoofdCategorieId { get; set; }
  
    [Display(Name = "Naam Van Hoofd Categorie")]

    public virtual Categorie? HoofdCategorie { get; set; }

    public virtual ICollection<Categorie> Subcategorieën { get; set; } = new List<Categorie>();

    public virtual ICollection<Artikel> Artikelen { get; set; } = new List<Artikel>();
}
