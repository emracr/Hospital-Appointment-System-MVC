using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RandevuSistemi.Models.Tables;

namespace RandevuSistemi.Models.Tables
{
    [Table("Poliklinik")]
    public class Poliklinik
    {
        public Poliklinik()
        {
            Hekimler = new List<Hekim>();
            Randevular = new List<Randevu>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30), Required]
        public string PoliklinikAdi { get; set; }
        public virtual List<Hekim> Hekimler { get; set; }
        public virtual List<Randevu> Randevular { get; set; }
    }
}