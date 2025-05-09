﻿using PrulariaAankoopData.Models;
using System.ComponentModel.DataAnnotations;

namespace PrulariaAankoopUI.Models;

public class CategorieViewModel
{
    public int CategorieId { get; set; }
    [Required(ErrorMessage = "De naam is verplicht.")]
    [Display(Name = "Naam")]
    public string Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }
    [Required(ErrorMessage = "De naam is verplicht.")]
    [Display(Name = "Hoofdcategorie")]
    public virtual Categorie? HoofdCategorie { get; set; }

    public virtual ICollection<Categorie> Subcategorieën { get; set; } = new List<Categorie>();

    public List<Categorie> Categorieen { get; set; } = new List<Categorie>();
    public CategorieArtikelViewModel ArtikelToevoegenForm { get; set; } = new();
}
