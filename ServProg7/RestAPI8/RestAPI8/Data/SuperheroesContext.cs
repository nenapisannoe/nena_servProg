using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestAPI8.Models;

namespace RestAPI8.Data;

public partial class SuperheroesContext : DbContext
{
    public SuperheroesContext()
    {
    }

    public SuperheroesContext(DbContextOptions<SuperheroesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alignment> Alignments { get; set; }

    public virtual DbSet<RestAPI8.Models.Attribute> Attributes { get; set; }

    public virtual DbSet<Colour> Colours { get; set; }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<HeroAttribute> HeroAttributes { get; set; }

    public virtual DbSet<HeroPower> HeroPowers { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Superhero> Superheroes { get; set; }

    public virtual DbSet<Superpower> Superpowers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Superheroes;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_alignment");

            entity.ToTable("alignment", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlignmentName)
                .HasMaxLength(10)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("alignment");
        });

        modelBuilder.Entity<RestAPI8.Models.Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_attribute");

            entity.ToTable("attribute", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttributeName)
                .HasMaxLength(200)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("attribute_name");
        });

        modelBuilder.Entity<Colour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_colour");

            entity.ToTable("colour", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Colour1)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("colour");
        });

        modelBuilder.Entity<Comic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_comic");

            entity.ToTable("comic", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComicName)
                .HasMaxLength(200)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("comic_name");
            entity.Property(e => e.Issue).HasColumnName("issue");
            entity.Property(e => e.PublishMonth).HasColumnName("publish_month");
            entity.Property(e => e.PublishYear).HasColumnName("publish_year");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_gender");

            entity.ToTable("gender", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gender1)
                .HasMaxLength(20)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("gender");
        });

        modelBuilder.Entity<HeroAttribute>(entity =>
        {
            entity.HasKey(e => new { e.AttributeId, e.HeroId }); // Указываем составной ключ

            entity.ToTable("hero_attribute", "superhero");

            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.AttributeValue).HasColumnName("attribute_value");
            entity.Property(e => e.HeroId).HasColumnName("hero_id");

            entity.HasOne(d => d.Attribute)
                .WithMany()
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Hero)
                .WithMany()
                .HasForeignKey(d => d.HeroId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<HeroPower>(entity =>
        {
            entity
                .HasKey(e => new { e.HeroId, e.PowerId });

            entity.ToTable("hero_power", "superhero");

            entity.Property(e => e.HeroId).HasColumnName("hero_id");
            entity.Property(e => e.PowerId).HasColumnName("power_id");

            entity.HasOne(d => d.Hero)
                .WithMany()
                .HasForeignKey(d => d.HeroId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure cascade delete behavior

            entity.HasOne(d => d.Power)
                .WithMany()
                .HasForeignKey(d => d.PowerId);
        });


        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_publisher");

            entity.ToTable("publisher", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("publisher_name");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_race");

            entity.ToTable("race", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Race1)
                .HasMaxLength(100)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("race");
        });

        modelBuilder.Entity<Superhero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_superhero");

            entity.ToTable("superhero", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlignmentId).HasColumnName("alignment_id");
            entity.Property(e => e.EyeColourId).HasColumnName("eye_colour_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("full_name");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.HairColourId).HasColumnName("hair_colour_id");
            entity.Property(e => e.HeightCm).HasColumnName("height_cm");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.RaceId).HasColumnName("race_id");
            entity.Property(e => e.SkinColourId).HasColumnName("skin_colour_id");
            entity.Property(e => e.SuperheroName)
                .HasMaxLength(200)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("superhero_name");
            entity.Property(e => e.WeightKg).HasColumnName("weight_kg");

            entity.HasOne(d => d.EyeColour).WithMany(p => p.SuperheroEyeColours)
                .HasForeignKey(d => d.EyeColourId)
                .HasConstraintName("fk_sup_eyecol");

            entity.HasOne(d => d.Gender).WithMany(p => p.Superheroes)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("fk_sup_gen");

            entity.HasOne(d => d.HairColour).WithMany(p => p.SuperheroHairColours)
                .HasForeignKey(d => d.HairColourId)
                .HasConstraintName("fk_sup_haircol");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Superheroes)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("fk_sup_pub");

            entity.HasOne(d => d.Race).WithMany(p => p.Superheroes)
                .HasForeignKey(d => d.RaceId)
                .HasConstraintName("fk_sup_race");

            entity.HasOne(d => d.SkinColour).WithMany(p => p.SuperheroSkinColours)
                .HasForeignKey(d => d.SkinColourId)
                .HasConstraintName("fk_sup_skincol");
        });

        modelBuilder.Entity<Superpower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_superpower");

            entity.ToTable("superpower", "superhero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PowerName)
                .HasMaxLength(200)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("power_name");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
