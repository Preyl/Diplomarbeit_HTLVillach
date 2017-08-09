namespace DataDictionary.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityDataModel : DbContext
    {
        public EntityDataModel()
            : base("name=EntityDataModel")
        {
        }

        public virtual DbSet<Anwendung> Anwendung { get; set; }
        public virtual DbSet<Datentyp> Datentyp { get; set; }
        public virtual DbSet<Feld> Feld { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anwendung>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Anwendung>()
                .Property(e => e.Beschreibung)
                .IsUnicode(false);

            modelBuilder.Entity<Anwendung>()
                .HasMany(e => e.MeineDatentypen)
                .WithMany(e => e.MeineAnwendungen)
                .Map(m => m.ToTable("Anwendung_Datentyp").MapLeftKey("AnwendungId").MapRightKey("DatentypId"));

            modelBuilder.Entity<Anwendung>()
                .HasMany(e => e.MeineFelder)
                .WithMany(e => e.MeineAnwendungen)
                .Map(m => m.ToTable("Anwendung_Feld").MapLeftKey("AnwendungId").MapRightKey("FeldId"));

            modelBuilder.Entity<Datentyp>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Datentyp>()
                .Property(e => e.Beschreibung)
                .IsUnicode(false);

            modelBuilder.Entity<Datentyp>()
                .HasMany(e => e.MeineFelder)
                .WithMany(e => e.MeineDatentypen)
                .Map(m => m.ToTable("Datentyp_Feld").MapLeftKey("DatentypId").MapRightKey("FeldId"));

            modelBuilder.Entity<Feld>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
