namespace PrulariaAankoop.Models;
public class ArtikelsPerSubcategorieViewModel
{
    public string? SubcategorieNaam { get; set; }
    public List<ArtikelShortViewModel> Artikels { get; set; } = new();
    public bool HeeftSubcategorieën { get; set; }



    //public int ArtikelId { get; set; }
    //public string? ArtikelNaam { get; set; }
    //public int CategorieId { get; set; }
    //public string? CategorieNaam { get; set; }


}

public class ArtikelShortViewModel
{
    public string Naam { get; set; } = string.Empty;
    public string Beschrijving { get; set; } = string.Empty;

    public int ArtikelId { get; set; }
    //public string? ArtikelNaam { get; set; }
    public int CategorieId { get; set; }
    //public string? CategorieNaam { get; set; }

}

public class ArtikelCategorieViewModel
{
    public int ArtikelId { get; set; }
    //public string? ArtikelNaam { get; set; }
    public int CategorieId { get; set; }
    //public string? CategorieNaam { get; set; }
}