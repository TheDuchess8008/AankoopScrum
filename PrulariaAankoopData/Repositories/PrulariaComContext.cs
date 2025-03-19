using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrulariaAankoopData.Models;

namespace PrulariaAankoopData.Repositories;

public class PrulariaComContext : DbContext
{
    // public PrulariaComContext()
    // {
    // }

    public PrulariaComContext(DbContextOptions<PrulariaComContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actiecode> Actiecodes { get; set; }

    public virtual DbSet<Adres> Adressen { get; set; }

    public virtual DbSet<Artikel> Artikelen { get; set; }

    public virtual DbSet<ArtikelLeveranciersInfolijn> Artikelleveranciersinfolijnen { get; set; }

    public virtual DbSet<Bestellijn> Bestellijnen { get; set; }

    public virtual DbSet<Bestelling> Bestellingen { get; set; }

    public virtual DbSet<BestellingsStatus> Bestellingsstatussen { get; set; }

    public virtual DbSet<Betaalwijze> Betaalwijzes { get; set; }

    public virtual DbSet<Categorie> Categorieen { get; set; }

    public virtual DbSet<Chatgesprek> Chatgesprekken { get; set; }

    public virtual DbSet<Chatgespreklijn> Chatgespreklijnen { get; set; }

    public virtual DbSet<Contactpersoon> Contactpersonen { get; set; }

    public virtual DbSet<EventWachtrijArtikel> Eventwachtrijartikelen { get; set; }

    public virtual DbSet<GebruikersAccount> Gebruikersaccounts { get; set; }

    public virtual DbSet<InkomendeLevering> Inkomendeleveringen { get; set; }

    public virtual DbSet<InkomendeLeveringslijn> Inkomendeleveringslijnen { get; set; }

    public virtual DbSet<Klant> Klanten { get; set; }

    public virtual DbSet<KlantenReview> Klantenreviews { get; set; }

    public virtual DbSet<Leverancier> Leveranciers { get; set; }

    public virtual DbSet<Magazijnplaats> Magazijnplaatsen { get; set; }

    public virtual DbSet<NatuurlijkePersoon> Natuurlijkepersonen { get; set; }

    public virtual DbSet<Personeelslid> Personeelsleden { get; set; }

    public virtual DbSet<Personeelslidaccount> Personeelslidaccounts { get; set; }

    public virtual DbSet<Plaats> Plaatsen { get; set; }

    public virtual DbSet<RechtsPersoon> Rechtspersonen { get; set; }

    public virtual DbSet<SecurityGroep> Securitygroepen { get; set; }

    public virtual DbSet<UitgaandeLevering> Uitgaandeleveringen { get; set; }

    public virtual DbSet<UitgaandeLeveringsStatus> Uitgaandeleveringsstatussen { get; set; }

    public virtual DbSet<Veelgesteldevragenartikel> Veelgesteldevragenartikels { get; set; }

    public virtual DbSet<Wishlistitem> Wishlistitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actiecode>(entity =>
        {
            entity.HasKey(e => e.ActiecodeId).HasName("PRIMARY");

            entity.ToTable("actiecodes");

            entity.Property(e => e.ActiecodeId).HasColumnName("actiecodeId");
            entity.Property(e => e.GeldigTotDatum)
                .HasColumnType("date")
                .HasColumnName("geldigTotDatum");
            entity.Property(e => e.GeldigVanDatum)
                .HasColumnType("date")
                .HasColumnName("geldigVanDatum");
            entity.Property(e => e.IsEenmalig).HasColumnName("isEenmalig");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Adres>(entity =>
        {
            entity.HasKey(e => e.AdresId).HasName("PRIMARY");

            entity.ToTable("adressen");

            entity.HasIndex(e => e.PlaatsId, "fk_Adressen_Plaatsen_idx");

            entity.Property(e => e.AdresId).HasColumnName("adresId");
            entity.Property(e => e.Actief)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("actief");
            entity.Property(e => e.Bus)
                .HasMaxLength(5)
                .HasColumnName("bus");
            entity.Property(e => e.HuisNummer)
                .HasMaxLength(5)
                .HasColumnName("huisNummer");
            entity.Property(e => e.PlaatsId).HasColumnName("plaatsId");
            entity.Property(e => e.Straat)
                .HasMaxLength(100)
                .HasColumnName("straat");

            entity.HasOne(d => d.Plaats).WithMany(p => p.Adressen)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Adressen_Plaatsen");
        });

        modelBuilder.Entity<Artikel>(entity =>
        {
            entity.HasKey(e => e.ArtikelId).HasName("PRIMARY");

            entity.ToTable("artikelen");

            entity.HasIndex(e => e.Ean, "ean_UNIQUE").IsUnique();

            entity.HasIndex(e => e.LeveranciersId, "fk_Artikelen_Leveranciers");

            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.AantalBesteldLeverancier).HasColumnName("aantalBesteldLeverancier");
            entity.Property(e => e.Beschrijving)
                .HasMaxLength(255)
                .HasColumnName("beschrijving");
            entity.Property(e => e.Bestelpeil).HasColumnName("bestelpeil");
            entity.Property(e => e.Ean)
                .HasMaxLength(13)
                .HasColumnName("ean");
            entity.Property(e => e.GewichtInGram).HasColumnName("gewichtInGram");
            entity.Property(e => e.LeveranciersId).HasColumnName("leveranciersId");
            entity.Property(e => e.Levertijd)
                .HasDefaultValueSql("'1'")
                .HasColumnName("levertijd");
            entity.Property(e => e.MaxAantalInMagazijnPlaats).HasColumnName("maxAantalInMagazijnPLaats");
            entity.Property(e => e.MaximumVoorraad).HasColumnName("maximumVoorraad");
            entity.Property(e => e.MinimumVoorraad).HasColumnName("minimumVoorraad");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
            entity.Property(e => e.Prijs).HasColumnName("prijs");
            entity.Property(e => e.Voorraad).HasColumnName("voorraad");

            entity.HasOne(d => d.Leverancier).WithMany(p => p.Artikelen)
                .HasForeignKey(d => d.LeveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Artikelen_Leveranciers");
        });

        modelBuilder.Entity<ArtikelLeveranciersInfolijn>(entity =>
        {
            entity.HasKey(e => new { e.ArtikelLeveranciersInfoLijnId, e.ArtikelId }).HasName("PRIMARY");

            entity.ToTable("artikelleveranciersinfolijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_ArtikelLeveranciersInfoLijnen_Artikelen1_idx");

            entity.Property(e => e.ArtikelLeveranciersInfoLijnId)
                .ValueGeneratedOnAdd()
                .HasColumnName("artikelLeveranciersInfoLijnId");
            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.Antwoord)
                .HasMaxLength(255)
                .HasColumnName("antwoord");
            entity.Property(e => e.Vraag)
                .HasMaxLength(255)
                .HasColumnName("vraag");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Artikelleveranciersinfolijnen)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ArtikelLeveranciersInfoLijnen_Artikelen1");
        });

        modelBuilder.Entity<Bestellijn>(entity =>
        {
            entity.HasKey(e => e.BestellijnId).HasName("PRIMARY");

            entity.ToTable("bestellijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_Bestellijnen_Artikelen1_idx");

            entity.HasIndex(e => e.BestelId, "fk_Bestellijnen_Bestellingen1_idx");

            entity.Property(e => e.BestellijnId).HasColumnName("bestellijnId");
            entity.Property(e => e.AantalBesteld).HasColumnName("aantalBesteld");
            entity.Property(e => e.AantalGeannuleerd).HasColumnName("aantalGeannuleerd");
            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.BestelId).HasColumnName("bestelId");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Bestellijnen)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellijnen_Artikelen1");

            entity.HasOne(d => d.Bestel).WithMany(p => p.Bestellijnen)
                .HasForeignKey(d => d.BestelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellijnen_Bestellingen1");
        });

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.HasKey(e => e.BestelId).HasName("PRIMARY");

            entity.ToTable("bestellingen");

            entity.HasIndex(e => e.FacturatieAdresId, "fk_Bestellingen_Adressen1_idx");

            entity.HasIndex(e => e.LeveringsAdresId, "fk_Bestellingen_Adressen2_idx");

            entity.HasIndex(e => e.BestellingsStatusId, "fk_Bestellingen_BestellingsStatussen1_idx");

            entity.HasIndex(e => e.BetaalwijzeId, "fk_Bestellingen_Betaalwijzes1_idx");

            entity.HasIndex(e => e.KlantId, "fk_Bestellingen_Klanten1_idx");

            entity.Property(e => e.BestelId).HasColumnName("bestelId");
            entity.Property(e => e.ActiecodeGebruikt).HasColumnName("actiecodeGebruikt");
            entity.Property(e => e.Annulatie).HasColumnName("annulatie");
            entity.Property(e => e.Annulatiedatum)
                .HasColumnType("date")
                .HasColumnName("annulatiedatum");
            entity.Property(e => e.Bedrijfsnaam)
                .HasMaxLength(45)
                .HasColumnName("bedrijfsnaam");
            entity.Property(e => e.Besteldatum)
                .HasColumnType("datetime")
                .HasColumnName("besteldatum");
            entity.Property(e => e.BestellingsStatusId).HasColumnName("bestellingsStatusId");
            entity.Property(e => e.Betaald).HasColumnName("betaald");
            entity.Property(e => e.BetaalwijzeId).HasColumnName("betaalwijzeId");
            entity.Property(e => e.Betalingscode)
                .HasMaxLength(45)
                .HasColumnName("betalingscode");
            entity.Property(e => e.BtwNummer)
                .HasMaxLength(45)
                .HasColumnName("btwNummer");
            entity.Property(e => e.FacturatieAdresId).HasColumnName("facturatieAdresId");
            entity.Property(e => e.Familienaam)
                .HasMaxLength(45)
                .HasColumnName("familienaam");
            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.LeveringsAdresId).HasColumnName("leveringsAdresId");
            entity.Property(e => e.Terugbetalingscode)
                .HasMaxLength(45)
                .HasColumnName("terugbetalingscode");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(45)
                .HasColumnName("voornaam");

            entity.HasOne(d => d.BestellingsStatus).WithMany(p => p.Bestellingen)
                .HasForeignKey(d => d.BestellingsStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_BestellingsStatussen1");

            entity.HasOne(d => d.Betaalwijze).WithMany(p => p.Bestellingen)
                .HasForeignKey(d => d.BetaalwijzeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Betaalwijzes1");

            entity.HasOne(d => d.FacturatieAdres).WithMany(p => p.BestellingenFacturatieAdres)
                .HasForeignKey(d => d.FacturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen1");

            entity.HasOne(d => d.Klant).WithMany(p => p.Bestellingen)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Klanten1");

            entity.HasOne(d => d.LeveringsAdres).WithMany(p => p.BestellingenLeveringsAdres)
                .HasForeignKey(d => d.LeveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Bestellingen_Adressen2");
        });

        modelBuilder.Entity<BestellingsStatus>(entity =>
        {
            entity.HasKey(e => e.BestellingsStatusId).HasName("PRIMARY");

            entity.ToTable("bestellingsstatussen");

            entity.Property(e => e.BestellingsStatusId).HasColumnName("bestellingsStatusId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Betaalwijze>(entity =>
        {
            entity.HasKey(e => e.BetaalwijzeId).HasName("PRIMARY");

            entity.ToTable("betaalwijzes");

            entity.Property(e => e.BetaalwijzeId).HasColumnName("betaalwijzeId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.CategorieId).HasName("PRIMARY");

            entity.ToTable("categorieen");

            entity.HasIndex(e => e.HoofdCategorieId, "fk_Categorieen_Categorieen1_idx");

            entity.Property(e => e.CategorieId).HasColumnName("categorieId");
            entity.Property(e => e.HoofdCategorieId).HasColumnName("hoofdCategorieId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");

            entity.HasOne(d => d.HoofdCategorie).WithMany(p => p.Subcategorieën)
                .HasForeignKey(d => d.HoofdCategorieId)
                .HasConstraintName("fk_Categorieen_Categorieen1");

            entity.HasMany(d => d.Artikelen).WithMany(p => p.Categorieën)
                .UsingEntity<Dictionary<string, object>>(
                    "Artikelcategorieen",
                    r => r.HasOne<Artikel>().WithMany()
                        .HasForeignKey("ArtikelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ArtikelCategorieen_Artikelen1"),
                    l => l.HasOne<Categorie>().WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ArtikelCategorieen_Categorieen1"),
                    j =>
                    {
                        j.HasKey("CategorieId", "ArtikelId").HasName("PRIMARY");
                        j.ToTable("artikelcategorieen");
                        j.HasIndex(new[] { "ArtikelId" }, "fk_ArtikelCategorieen_Artikelen1_idx");
                        j.IndexerProperty<int>("CategorieId").HasColumnName("categorieId");
                        j.IndexerProperty<int>("ArtikelId").HasColumnName("artikelId");
                    });
        });

        modelBuilder.Entity<Chatgesprek>(entity =>
        {
            entity.HasKey(e => e.ChatgesprekId).HasName("PRIMARY");

            entity.ToTable("chatgesprekken");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_ChatGesprekken_GebruikersAccounts1_idx");

            entity.Property(e => e.ChatgesprekId).HasColumnName("chatgesprekId");
            entity.Property(e => e.GebruikersAccountId).HasColumnName("gebruikersAccountId");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Chatgesprekken)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChatGesprekken_GebruikersAccounts1");
        });

        modelBuilder.Entity<Chatgespreklijn>(entity =>
        {
            entity.HasKey(e => e.ChatgesprekLijnId).HasName("PRIMARY");

            entity.ToTable("chatgespreklijnen");

            entity.HasIndex(e => e.ChatgesprekId, "fk_ChatgesprekLijnen_ChatGesprekken1_idx");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_ChatgesprekLijnen_GebruikersAccounts1_idx");

            entity.HasIndex(e => e.PersoneelslidAccountId, "fk_ChatgesprekLijnen_PersoneelslidAccounts1_idx");

            entity.Property(e => e.ChatgesprekLijnId).HasColumnName("chatgesprekLijnId");
            entity.Property(e => e.Bericht)
                .HasMaxLength(255)
                .HasColumnName("bericht");
            entity.Property(e => e.ChatgesprekId).HasColumnName("chatgesprekId");
            entity.Property(e => e.GebruikersAccountId)
                .HasDefaultValueSql("'0'")
                .HasColumnName("gebruikersAccountId");
            entity.Property(e => e.PersoneelslidAccountId)
                .HasDefaultValueSql("'0'")
                .HasColumnName("personeelslidAccountId");
            entity.Property(e => e.Tijdstip)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("tijdstip");

            entity.HasOne(d => d.Chatgesprek).WithMany(p => p.Chatgespreklijnen)
                .HasForeignKey(d => d.ChatgesprekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChatgesprekLijnen_ChatGesprekken1");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Chatgespreklijnen)
                .HasForeignKey(d => d.GebruikersAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_GebruikersAccounts1");

            entity.HasOne(d => d.PersoneelslidAccount).WithMany(p => p.Chatgespreklijnen)
                .HasForeignKey(d => d.PersoneelslidAccountId)
                .HasConstraintName("fk_ChatgesprekLijnen_PersoneelslidAccounts1");
        });

        modelBuilder.Entity<Contactpersoon>(entity =>
        {
            entity.HasKey(e => e.ContactpersoonId).HasName("PRIMARY");

            entity.ToTable("contactpersonen");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_Contactpersonen_GebruikersAccounts_idx");

            entity.HasIndex(e => e.KlantId, "fk_Contactpersonen_Rechtspersonen1_idx");

            entity.Property(e => e.ContactpersoonId).HasColumnName("contactpersoonId");
            entity.Property(e => e.Familienaam)
                .HasMaxLength(45)
                .HasColumnName("familienaam");
            entity.Property(e => e.Functie)
                .HasMaxLength(45)
                .HasColumnName("functie");
            entity.Property(e => e.GebruikersAccountId).HasColumnName("gebruikersAccountId");
            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(45)
                .HasColumnName("voornaam");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Contactpersonen)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contactpersonen_GebruikersAccounts1");

            entity.HasOne(d => d.Klant).WithMany(p => p.Contactpersonen)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Contactpersonen_Rechtspersonen1");
        });

        modelBuilder.Entity<EventWachtrijArtikel>(entity =>
        {
            entity.HasKey(e => e.ArtikelId).HasName("PRIMARY");

            entity.ToTable("eventwachtrijartikelen");

            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.Aantal).HasColumnName("aantal");
            entity.Property(e => e.MaxAantalInMagazijnPlaats).HasColumnName("maxAantalInMagazijnPlaats");
        });

        modelBuilder.Entity<GebruikersAccount>(entity =>
        {
            entity.HasKey(e => e.GebruikersAccountId).HasName("PRIMARY");

            entity.ToTable("gebruikersaccounts");

            entity.HasIndex(e => e.Emailadres, "gebrukersnaam_UNIQUE").IsUnique();

            entity.Property(e => e.GebruikersAccountId).HasColumnName("gebruikersAccountId");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Emailadres)
                .HasMaxLength(45)
                .HasColumnName("emailadres");
            entity.Property(e => e.Paswoord)
                .HasMaxLength(255)
                .HasColumnName("paswoord");
        });

        modelBuilder.Entity<InkomendeLevering>(entity =>
        {
            entity.HasKey(e => e.InkomendeLeveringsId).HasName("PRIMARY");

            entity.ToTable("inkomendeleveringen");

            entity.HasIndex(e => e.LeveranciersId, "fk_InkomendeLeveringen_Leveranciers1");

            entity.HasIndex(e => e.OntvangerPersoneelslidId, "fk_InkomendeLeveringen_Personeelsleden1_idx");

            entity.Property(e => e.InkomendeLeveringsId).HasColumnName("inkomendeLeveringsId");
            entity.Property(e => e.LeverDatum)
                .HasColumnType("date")
                .HasColumnName("leverDatum");
            entity.Property(e => e.LeveranciersId).HasColumnName("leveranciersId");
            entity.Property(e => e.LeveringsbonNummer)
                .HasMaxLength(45)
                .HasColumnName("leveringsbonNummer");
            entity.Property(e => e.Leveringsbondatum)
                .HasColumnType("date")
                .HasColumnName("leveringsbondatum");
            entity.Property(e => e.OntvangerPersoneelslidId).HasColumnName("ontvangerPersoneelslidId");

            entity.HasOne(d => d.Leverancier).WithMany(p => p.InkomendeLeveringen)
                .HasForeignKey(d => d.LeveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeveringen_Leveranciers1");

            entity.HasOne(d => d.OntvangerPersoneelslid).WithMany(p => p.InkomendeLeveringen)
                .HasForeignKey(d => d.OntvangerPersoneelslidId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeveringen_Personeelsleden1");
        });

        modelBuilder.Entity<InkomendeLeveringslijn>(entity =>
        {
            entity.HasKey(e => new { e.InkomendeLeveringsId, e.ArtikelId, e.MagazijnPlaatsId }).HasName("PRIMARY");

            entity.ToTable("inkomendeleveringslijnen");

            entity.HasIndex(e => e.ArtikelId, "fk_InkomendeLeverongsLijnen_Artikelen1_idx");

            entity.HasIndex(e => e.MagazijnPlaatsId, "fk_InkomendeLeverongsLijnen_MagazijnPlaatsen1_idx");

            entity.Property(e => e.InkomendeLeveringsId).HasColumnName("inkomendeLeveringsId");
            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.MagazijnPlaatsId).HasColumnName("magazijnPlaatsId");
            entity.Property(e => e.AantalGoedgekeurd).HasColumnName("aantalGoedgekeurd");
            entity.Property(e => e.AantalTeruggestuurd).HasColumnName("aantalTeruggestuurd");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Inkomendeleveringslijnen)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_Artikelen1");

            entity.HasOne(d => d.InkomendeLevering).WithMany(p => p.Inkomendeleveringslijnen)
                .HasForeignKey(d => d.InkomendeLeveringsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_InkomendeLeveringen1");

            entity.HasOne(d => d.MagazijnPlaats).WithMany(p => p.InkomendeLeveringslijnen)
                .HasForeignKey(d => d.MagazijnPlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InkomendeLeverongsLijnen_MagazijnPlaatsen1");
        });

        modelBuilder.Entity<Klant>(entity =>
        {
            entity.HasKey(e => e.KlantId).HasName("PRIMARY");

            entity.ToTable("klanten");

            entity.HasIndex(e => e.FacturatieAdresId, "fk_Klanten_Adressen1_idx");

            entity.HasIndex(e => e.LeveringsAdresId, "fk_Klanten_Adressen2_idx");

            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.FacturatieAdresId).HasColumnName("facturatieAdresId");
            entity.Property(e => e.LeveringsAdresId).HasColumnName("leveringsAdresId");

            entity.HasOne(d => d.FacturatieAdres).WithMany(p => p.KlantenFacturatieAdres)
                .HasForeignKey(d => d.FacturatieAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klanten_Adressen1");

            entity.HasOne(d => d.LeveringsAdres).WithMany(p => p.KlantenLeveringsAdres)
                .HasForeignKey(d => d.LeveringsAdresId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Klanten_Adressen2");
        });

        modelBuilder.Entity<KlantenReview>(entity =>
        {
            entity.HasKey(e => e.KlantenReviewId).HasName("PRIMARY");

            entity.ToTable("klantenreviews");

            entity.HasIndex(e => e.BestellijnId, "fk_KlantenReviews_Bestellijnen1_idx");

            entity.Property(e => e.KlantenReviewId).HasColumnName("klantenReviewId");
            entity.Property(e => e.BestellijnId).HasColumnName("bestellijnId");
            entity.Property(e => e.Commentaar)
                .HasMaxLength(255)
                .HasColumnName("commentaar");
            entity.Property(e => e.Datum)
                .HasColumnType("date")
                .HasColumnName("datum");
            entity.Property(e => e.Nickname)
                .HasMaxLength(45)
                .HasColumnName("nickname");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.Bestellijn).WithMany(p => p.Klantenreviews)
                .HasForeignKey(d => d.BestellijnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_KlantenReviews_Bestellijnen1");
        });

        modelBuilder.Entity<Leverancier>(entity =>
        {
            entity.HasKey(e => e.LeveranciersId).HasName("PRIMARY");

            entity.ToTable("leveranciers");

            entity.HasIndex(e => e.PlaatsId, "fk_Leveranciers_Plaatsen1_idx");

            entity.Property(e => e.LeveranciersId).HasColumnName("leveranciersId");
            entity.Property(e => e.BtwNummer)
                .HasMaxLength(45)
                .HasColumnName("btwNummer");
            entity.Property(e => e.Bus)
                .HasMaxLength(5)
                .HasColumnName("bus");
            entity.Property(e => e.FamilienaamContactpersoon)
                .HasMaxLength(45)
                .HasColumnName("familienaamContactpersoon");
            entity.Property(e => e.HuisNummer)
                .HasMaxLength(5)
                .HasColumnName("huisNummer");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
            entity.Property(e => e.PlaatsId).HasColumnName("plaatsId");
            entity.Property(e => e.Straat)
                .HasMaxLength(45)
                .HasColumnName("straat");
            entity.Property(e => e.VoornaamContactpersoon)
                .HasMaxLength(45)
                .HasColumnName("voornaamContactpersoon");

            entity.HasOne(d => d.Plaats).WithMany(p => p.Leveranciers)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Leveranciers_Plaatsen1");
        });

        modelBuilder.Entity<Magazijnplaats>(entity =>
        {
            entity.HasKey(e => e.MagazijnPlaatsId).HasName("PRIMARY");

            entity.ToTable("magazijnplaatsen");

            entity.HasIndex(e => e.ArtikelId, "fk_MagazijnPlaatsen_Artikelen1_idx");

            entity.HasIndex(e => new { e.Rij, e.Rek }, "uinx_rijrek").IsUnique();

            entity.Property(e => e.MagazijnPlaatsId).HasColumnName("magazijnPlaatsId");
            entity.Property(e => e.Aantal).HasColumnName("aantal");
            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.Rek).HasColumnName("rek");
            entity.Property(e => e.Rij)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("rij");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Magazijnplaatsen)
                .HasForeignKey(d => d.ArtikelId)
                .HasConstraintName("fk_MagazijnPlaatsen_Artikelen1");
        });

        modelBuilder.Entity<NatuurlijkePersoon>(entity =>
        {
            entity.HasKey(e => e.KlantId).HasName("PRIMARY");

            entity.ToTable("natuurlijkepersonen");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_NatuurlijkePersonen_gebruikersAccountId_idx");

            entity.HasIndex(e => e.KlantId, "fk_PrivateKlanten_Klanten1_idx");

            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.Familienaam)
                .HasMaxLength(45)
                .HasColumnName("familienaam");
            entity.Property(e => e.GebruikersAccountId).HasColumnName("gebruikersAccountId");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(45)
                .HasColumnName("voornaam");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Natuurlijkepersonen)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_NatuurlijkePersonen_Gebruikersnamen1");

            entity.HasOne(d => d.Klant).WithOne(p => p.NatuurlijkePersoon)
                .HasForeignKey<NatuurlijkePersoon>(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PrivateKlanten_Klanten1");
        });

        modelBuilder.Entity<Personeelslid>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidId).HasName("PRIMARY");

            entity.ToTable("personeelsleden");

            entity.HasIndex(e => e.PersoneelslidAccountId, "fk_Personeelsleden_PersoneelslidAccounts1_idx");

            entity.Property(e => e.PersoneelslidId).HasColumnName("personeelslidId");
            entity.Property(e => e.Familienaam)
                .HasMaxLength(45)
                .HasColumnName("familienaam");
            entity.Property(e => e.InDienst)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("inDienst");
            entity.Property(e => e.PersoneelslidAccountId).HasColumnName("personeelslidAccountId");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(45)
                .HasColumnName("voornaam");

            entity.HasOne(d => d.PersoneelslidAccount).WithOne(p => p.Personeelslid)
                .HasForeignKey<Personeelslid>(d => d.PersoneelslidAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Personeelsleden_PersoneelslidAccounts1");

            entity.HasMany(d => d.SecurityGroepen).WithMany(p => p.Personeelsleden)
                .UsingEntity<Dictionary<string, object>>(
                    "Personeelslidsecuritygroepen",
                    r => r.HasOne<SecurityGroep>().WithMany()
                        .HasForeignKey("SecurityGroepId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_SecurityGroepen1"),
                    l => l.HasOne<Personeelslid>().WithMany()
                        .HasForeignKey("PersoneelslidId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_Personeelsleden1"),
                    j =>
                    {
                        j.HasKey("PersoneelslidId", "SecurityGroepId").HasName("PRIMARY");
                        j.ToTable("personeelslidsecuritygroepen");
                        j.HasIndex(new[] { "SecurityGroepId" }, "fk_PersoneelslidSecurityGroepen_SecurityGroepen1_idx");
                        j.IndexerProperty<int>("PersoneelslidId").HasColumnName("personeelslidId");
                        j.IndexerProperty<int>("SecurityGroepId").HasColumnName("securityGroepId");
                    });
        });

        modelBuilder.Entity<Personeelslidaccount>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidAccountId).HasName("PRIMARY");

            entity.ToTable("personeelslidaccounts");

            entity.HasIndex(e => e.Emailadres, "emailadres_UNIQUE").IsUnique();

            entity.Property(e => e.PersoneelslidAccountId).HasColumnName("personeelslidAccountId");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Emailadres)
                .HasMaxLength(45)
                .HasColumnName("emailadres");
            entity.Property(e => e.Paswoord)
                .HasMaxLength(255)
                .HasColumnName("paswoord");
        });

        modelBuilder.Entity<Plaats>(entity =>
        {
            entity.HasKey(e => e.PlaatsId).HasName("PRIMARY");

            entity.ToTable("plaatsen");

            entity.Property(e => e.PlaatsId).HasColumnName("plaatsId");
            entity.Property(e => e.Naam)
                .HasMaxLength(150)
                .HasColumnName("plaats");
            entity.Property(e => e.Postcode)
                .HasMaxLength(4)
                .HasColumnName("postcode");
        });

        modelBuilder.Entity<RechtsPersoon>(entity =>
        {
            entity.HasKey(e => e.KlantId).HasName("PRIMARY");

            entity.ToTable("rechtspersonen");

            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.BtwNummer)
                .HasMaxLength(10)
                .HasColumnName("btwNummer");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");

            entity.HasOne(d => d.Klant).WithOne(p => p.RechtsPersoon)
                .HasForeignKey<RechtsPersoon>(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rechtspersonen_Klanten1");
        });

        modelBuilder.Entity<SecurityGroep>(entity =>
        {
            entity.HasKey(e => e.SecurityGroepId).HasName("PRIMARY");

            entity.ToTable("securitygroepen");

            entity.Property(e => e.SecurityGroepId).HasColumnName("securityGroepId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<UitgaandeLevering>(entity =>
        {
            entity.HasKey(e => e.UitgaandeLeveringsId).HasName("PRIMARY");

            entity.ToTable("uitgaandeleveringen");

            entity.HasIndex(e => e.BestelId, "fk_UitgaandeLeveringen_Bestellingen1_idx");

            entity.HasIndex(e => e.KlantId, "fk_UitgaandeLeveringen_Klanten1_idx");

            entity.HasIndex(e => e.UitgaandeLeveringsStatusId, "fk_UitgaandeLeveringen_UitgaandeLeveringsStatussn1_idx");

            entity.Property(e => e.UitgaandeLeveringsId).HasColumnName("uitgaandeLeveringsId");
            entity.Property(e => e.AankomstDatum)
                .HasColumnType("date")
                .HasColumnName("aankomstDatum");
            entity.Property(e => e.BestelId).HasColumnName("bestelId");
            entity.Property(e => e.KlantId).HasColumnName("klantId");
            entity.Property(e => e.Trackingcode)
                .HasMaxLength(45)
                .HasColumnName("trackingcode");
            entity.Property(e => e.UitgaandeLeveringsStatusId).HasColumnName("uitgaandeLeveringsStatusId");
            entity.Property(e => e.VertrekDatum)
                .HasColumnType("date")
                .HasColumnName("vertrekDatum");

            entity.HasOne(d => d.Bestel).WithMany(p => p.Uitgaandeleveringen)
                .HasForeignKey(d => d.BestelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_Bestellingen1");

            entity.HasOne(d => d.Klant).WithMany(p => p.UitgaandeLeveringen)
                .HasForeignKey(d => d.KlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_Klanten1");

            entity.HasOne(d => d.UitgaandeLeveringsStatus).WithMany(p => p.UitgaandeLeveringen)
                .HasForeignKey(d => d.UitgaandeLeveringsStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UitgaandeLeveringen_UitgaandeLeveringsStatussn1");
        });

        modelBuilder.Entity<UitgaandeLeveringsStatus>(entity =>
        {
            entity.HasKey(e => e.UitgaandeLeveringsStatusId).HasName("PRIMARY");

            entity.ToTable("uitgaandeleveringsstatussen");

            entity.Property(e => e.UitgaandeLeveringsStatusId).HasColumnName("uitgaandeLeveringsStatusId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Veelgesteldevragenartikel>(entity =>
        {
            entity.HasKey(e => e.VeelgesteldeVragenArtikelId).HasName("PRIMARY");

            entity.ToTable("veelgesteldevragenartikels");

            entity.HasIndex(e => e.ArtikelId, "fk_VeelgesteldeVragenArtikels_Artikelen1_idx");

            entity.Property(e => e.VeelgesteldeVragenArtikelId).HasColumnName("veelgesteldeVragenArtikelId");
            entity.Property(e => e.Antwoord)
                .HasMaxLength(255)
                .HasColumnName("antwoord");
            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.Vraag)
                .HasMaxLength(255)
                .HasColumnName("vraag");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Veelgesteldevragenartikel)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VeelgesteldeVragenArtikels_Artikelen1");
        });

        modelBuilder.Entity<Wishlistitem>(entity =>
        {
            entity.HasKey(e => new { e.WishListItemId, e.GebruikersAccountId }).HasName("PRIMARY");

            entity.ToTable("wishlistitems");

            entity.HasIndex(e => e.ArtikelId, "fk_WishListItems_Artikelen1_idx");

            entity.HasIndex(e => e.GebruikersAccountId, "fk_WishListItems_GebruikersAccounts1_idx");

            entity.Property(e => e.WishListItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("wishListItemId");
            entity.Property(e => e.GebruikersAccountId).HasColumnName("gebruikersAccountId");
            entity.Property(e => e.Aantal)
                .HasDefaultValueSql("'1'")
                .HasColumnName("aantal");
            entity.Property(e => e.AanvraagDatum)
                .HasColumnType("date")
                .HasColumnName("aanvraagDatum");
            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.EmailGestuurdDatum)
                .HasColumnType("date")
                .HasColumnName("emailGestuurdDatum");

            entity.HasOne(d => d.Artikel).WithMany(p => p.Wishlistitems)
                .HasForeignKey(d => d.ArtikelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WishListItems_Artikelen1");

            entity.HasOne(d => d.GebruikersAccount).WithMany(p => p.Wishlistitems)
                .HasForeignKey(d => d.GebruikersAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WishListItems_GebruikersAccounts1");
        });

        // OnModelCreatingPartial(modelBuilder);
    }

    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
