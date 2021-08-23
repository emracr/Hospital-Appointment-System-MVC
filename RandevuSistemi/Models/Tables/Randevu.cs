using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RandevuSistemi.Models.Tables
{
    [Table("Randevu")]
    public class Randevu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HastaTC { get; set; }
        public System.DateTime RandevuTarihi { get; set; }
        public int RandevuSaati { get; set; }
        public System.DateTime RandevuAlmaTarihi { get; set; }
        public int Poliklinik { get; set; }
        public string Hekim { get; set; }
        public int Il { get; set; }
        public string Ilce { get; set; }

        [ForeignKey("Poliklinik")]
        public virtual Poliklinik Poliklinikler { get; set; }

        [ForeignKey("RandevuSaati")]
        public virtual RandevuSaat RandevuSaatleri { get; set; }

        [ForeignKey("Il")]
        public virtual Il Iller { get; set; }

        [ForeignKey("HastaTC")]
        public virtual Vatandas Vatandaslar { get; set; }
    }
}