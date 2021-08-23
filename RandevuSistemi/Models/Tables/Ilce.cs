using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using RandevuSistemi.Models.Tables;

namespace RandevuSistemi.Models.Tables
{
    [Table("Ilce")]
    public class Ilce
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30), Required]
        public string IlceAdi { get; set; }
        public int SehirId { get; set; }

        public virtual Il Iller { get; set; }
    }
}