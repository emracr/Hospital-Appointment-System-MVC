using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RandevuSistemi.Models.Tables;

namespace RandevuSistemi.Models.Tables
{
    [Table("Il")]
    public class Il
    {
        public Il()
        {
            Ilceler = new List<Ilce>();
            Randevular = new List<Randevu>();
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string SehirAdi { get; set; }
        public virtual List<Ilce> Ilceler { get; set; }
        public virtual List<Randevu> Randevular { get; set; }
    }
}