using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RandevuSistemi.Models.Tables;
using System.ComponentModel.DataAnnotations;

namespace RandevuSistemi.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        [Required(ErrorMessage = "TC Kimlik boş bırakılamaz")]
        public string TC { get; set; }

        [Required(ErrorMessage = "Ad boş bırakılamaz")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz")]
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

        [Required(ErrorMessage = "Parola Tekrarı boş bırakılamaz")]
        [Compare("Parola", ErrorMessage = "Parolalar aynı değil lütfen kontrol edin")]
        public string ParolaTekrar { get; set; }
    }
}