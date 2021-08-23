using RandevuSistemi.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RandevuSistemi.ViewModels
{
    public class GeneralViewModel
    {
        public List<Hekim> HekimListe { get; set; }
        public List<Il> IlListe { get; set; }
        public List<Ilce> IlceListe { get; set; }
        public List<Poliklinik> PoliklinikListe { get; set; }
        public List<Randevu> RandevuListe { get; set; }
        public List<RandevuSaat> RandevuSaatListe { get; set; }
        public List<Vatandas> VatandasListe { get; set; }
    }
}