using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("liftmarketConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bayi> Bayi { get; set; }
        public DbSet<Hammadde> Hammadde { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<OdemeYontemleri> OdemeYontemleri { get; set; }
        public DbSet<Recete> Recete { get; set; }
        public DbSet<Sehir> Sehir { get; set; }
        public DbSet<ServisBakim> ServisBakim { get; set; }
        public DbSet<Siparisler> Siparisler { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Yonetici> Yonetici { get; set; }

    }
}