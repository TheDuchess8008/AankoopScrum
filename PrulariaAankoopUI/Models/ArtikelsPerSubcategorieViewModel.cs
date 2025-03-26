namespace PrulariaAankoop.Models;
public class ArtikelsPerSubcategorieViewModel
{
    public string? SubcategorieNaam { get; set; }
    public List<ArtikelShortViewModel> Artikels { get; set; } = new();
    public bool HeeftSubcategorieën { get; set; }

}

public class ArtikelShortViewModel
{
    public string Naam { get; set; } = string.Empty;
    public string Beschrijving { get; set; } = string.Empty;
}
