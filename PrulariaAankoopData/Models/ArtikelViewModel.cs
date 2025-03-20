namespace PrulariaAankoopData.Models
{
    public class ArtikelViewModel
    {
        public int ArtikelId { get; set; }
        public string Ean { get; set; } = null!;
        public string Naam { get; set; } = null!;
        public string Beschrijving { get; set; } = null!;
        public decimal Prijs { get; set; }
        public int GewichtInGram { get; set; }
        public int Bestelpeil { get; set; }
        public int Voorraad { get; set; }
        public int MinimumVoorraad { get; set; }
        public int MaximumVoorraad { get; set; }
        public int Levertijd { get; set; }
        public int AantalBesteldLeverancier { get; set; }
        public int MaxAantalInMagazijnPlaats { get; set; }
        public int LeveranciersId { get; set; }

        // Related data
        public string LeverancierNaam { get; set; } = null!;

        // Confirmation message (if applicable)
        public bool IsUpdateSuccessful { get; set; }
        public string ConfirmationMessage { get; set; } = null!;
    }
}
