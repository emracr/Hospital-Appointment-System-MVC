using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RandevuSistemi.ViewModels
{
    public class MakeAppointmentViewModel
    {
        [Required(ErrorMessage = "Lütfen bir İl seçiniz!")]
        public int SehirId { get; set; }
        [Required(ErrorMessage = "Lütfen bir İlçe seçiniz!")]
        public int ilceId { get; set; }
        public string HastaTC { get; set; }
        [Required(ErrorMessage = "Randevu Tarihi boş bırakılamaz!")]
        public System.DateTime RandevuTarihi { get; set; }
        [Required(ErrorMessage = "Randevu Saati boş bırakılamaz!")]
        public System.TimeSpan RandevuSaati { get; set; }
        [Required(ErrorMessage = "Poliklinik boş bırakılamaz!")]
        public string Poliklinik { get; set; }
        [Required(ErrorMessage = "Lütfen bir Hekim seçiniz!")]
        public string Hekim { get; set; }
        public System.DateTime RandevuAlmaTarihi { get; set; }
        [Required(ErrorMessage = "Lütfen bir İl seçiniz!")]
        public string il { get; set; }
        [Required(ErrorMessage = "Lütfen bir İlçe seçiniz!")]
        public string ilce { get; set; }
    }
}