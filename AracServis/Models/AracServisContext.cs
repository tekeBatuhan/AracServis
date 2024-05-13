using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AracServis.Models
{
    public partial class AracServisContext : DbContext
    {
        public AracServisContext()
            : base("name=AracServisContext")
        {
        }

        public virtual DbSet<AracBilgi> AracBilgi { get; set; }
        public virtual DbSet<Islem> Islem { get; set; }
        public virtual DbSet<IslemTur> IslemTur { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Unvan> Unvan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IslemTur>()
                .Property(e => e.fiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Musteri>()
                .HasMany(e => e.AracBilgi)
                .WithRequired(e => e.Musteri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unvan>()
                .HasMany(e => e.Kullanici)
                .WithRequired(e => e.Unvan)
                .WillCascadeOnDelete(false);
        }
    }
}
