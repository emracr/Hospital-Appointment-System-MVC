using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RandevuSistemi.Models.Tables
{
    [Table("Hekim")]
    public class Hekim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Isim { get; set; }
        public int Brans { get; set; }

        [ForeignKey("Brans")]
        public virtual Poliklinik Poliklinikler { get; set; }
    }
}