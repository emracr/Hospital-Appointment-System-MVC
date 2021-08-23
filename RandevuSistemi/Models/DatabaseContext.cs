using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RandevuSistemi.Models.Tables;

namespace RandevuSistemi.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Hekim> HekimTable { get; set; }
        public DbSet<Il> IlTable { get; set; }
        public DbSet<Ilce> IlceTable { get; set; }
        public DbSet<Poliklinik> PoliklinikTable { get; set; }
        public DbSet<Randevu> RandevuTable { get; set; }
        public DbSet<RandevuSaat> RandevuSaatTable { get; set; }
        public DbSet<Vatandas> VatandasTable { get; set; }
    }
}