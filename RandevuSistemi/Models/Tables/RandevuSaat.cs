using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RandevuSistemi.Models.Tables
{
    [Table("RandevuSaat")]
    public class RandevuSaat
    {
        public RandevuSaat()
        {
            Randevular = new List<Randevu>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50), Required]
        public string RandevuSaati { get; set; }

        public virtual List<Randevu> Randevular { get; set; }
    }
}