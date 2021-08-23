using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RandevuSistemi.Models.Tables
{
    [Table("Vatandas")]
    public class Vatandas
    {
        public Vatandas()
        {
            Randevular = new List<Randevu>();
        }

        [Key]
        [Required(ErrorMessage="TC Kimlik boş bırakılamaz")]
        public string TC { get; set; }

        [Required(ErrorMessage="Ad boş bırakılamaz")]
        public string Ad { get; set; }

        [Required(ErrorMessage="Soyad boş bırakılamaz")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi boş bırakılamaz")]
        public System.DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "Cinsiyet boş bırakılamaz")]
        public string Cinsiyet { get; set; }

        [Required(ErrorMessage = "E Mail boş bırakılamaz")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Telefon boş bırakılamaz")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Güvenlik Sorusu boş bırakılamaz")]
        public string GuvenlikSorusu { get; set; }

        [Required(ErrorMessage = "Güvenlik Cevabı boş bırakılamaz")]
        public string GuvenlikCevabi { get; set; }

        [Required(ErrorMessage = "Parola boş bırakılamaz")]
        public string Parola { get; set; }

        public string Role { get; set; }

        public virtual List<Randevu> Randevular { get; set; }
    }
}