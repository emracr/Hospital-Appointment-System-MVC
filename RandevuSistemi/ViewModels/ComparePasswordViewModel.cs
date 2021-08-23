using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RandevuSistemi.ViewModels
{
    public class ComparePasswordViewModel
    {
        [Required(ErrorMessage = "Parola boş bırakılamaz")]
        public string Parola { get; set; }

        [Required(ErrorMessage = "Parola Tekrarı boş bırakılamaz")]
        [Compare("Parola", ErrorMessage = "Parolalar aynı değil lütfen kontrol edin")]
        public string ParolaTekrar { get; set; }
        public string TC { get; set; }

        [Required(ErrorMessage = "Mevcut Parola boş bırakılamaz")]
        public string mevcutParola { get; set; }
    }
}