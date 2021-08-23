using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RandevuSistemi.Models.Tables;
using System.Data.Entity;
using RandevuSistemi.Models;
using RandevuSistemi.ViewModels;

namespace RandevuSistemi.Controllers
{
    public class AppointmentController : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();

        [HttpGet]
        public ActionResult MakeAnAppointment()
        {

            List<Il> Cities = _databaseContext.IlTable.ToList();
            ViewBag.Cities = new SelectList(Cities, "Id", "SehirAdi");

            List<Poliklinik> Poliklinikler = _databaseContext.PoliklinikTable.ToList();
            ViewBag.Poliklinikler = new SelectList(Poliklinikler, "Id", "PoliklinikAdi");

            List<RandevuSaat> RandevuSaatleri = _databaseContext.RandevuSaatTable.ToList();
            ViewBag.RandevuSaatleri = new SelectList(RandevuSaatleri, "Id", "RandevuSaati");

            return View();

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MakeAnAppointment(Randevu randevu)
        {
            if (!ModelState.IsValid)
            {
                return View("MakeAnAppointment");
            }

            List<Il> Cities = _databaseContext.IlTable.ToList();
            ViewBag.Cities = new SelectList(Cities, "Id", "SehirAdi");

            List<Poliklinik> Poliklinikler = _databaseContext.PoliklinikTable.ToList();
            ViewBag.Poliklinikler = new SelectList(Poliklinikler, "Id", "PoliklinikAdi");

            List<RandevuSaat> RandevuSaatleri = _databaseContext.RandevuSaatTable.ToList();
            ViewBag.RandevuSaatleri = new SelectList(RandevuSaatleri, "Id", "RandevuSaati");


            var kontrol = _databaseContext.RandevuTable.Where(m => m.RandevuTarihi == randevu.RandevuTarihi && m.RandevuSaati == randevu.RandevuSaati && m.Poliklinik == randevu.Poliklinik && m.Hekim == randevu.Hekim && m.Il == randevu.Il && m.Ilce == randevu.Ilce).ToList();
            var kontrol2 = _databaseContext.RandevuTable.Where(m=>m.HastaTC == HttpContext.User.Identity.Name && m.RandevuTarihi == randevu.RandevuTarihi && m.RandevuSaati == randevu.RandevuSaati).FirstOrDefault();
            var kontrol3 = _databaseContext.RandevuTable.Where(m => m.HastaTC == HttpContext.User.Identity.Name && m.RandevuTarihi == randevu.RandevuTarihi).ToList();
            var kontrol4 = _databaseContext.RandevuTable.Where(m => m.HastaTC == HttpContext.User.Identity.Name && m.RandevuTarihi == randevu.RandevuTarihi && m.Poliklinik == randevu.Poliklinik).FirstOrDefault();

            if (kontrol4 != null)
            {
                ViewBag.AppointmentPoliclinicOverflow = kontrol4.RandevuTarihi.ToShortDateString() + " günü " + kontrol4.Poliklinikler.PoliklinikAdi + " bölümüne zaten randevunuz var";
                return View();
            }
            if (kontrol3.Count >= 2)
            {
                ViewBag.AppointmentDailyOverflow = "Günlük maximum randevu sayısına ulaştınız";
                return View();
            }
            if (kontrol2 != null)
            {
                ViewBag.AppointmentConflict = kontrol2.RandevuTarihi.ToShortDateString() + " günü saat " + kontrol2.RandevuSaatleri.RandevuSaati + "'da başka randevunuz var" ;
                return View();
            }
            if (kontrol.Count >= 3)
            {
                ViewBag.AppointmentOverflow = "Malesef bu saatte boş randevu bulunmamaktadır";
                return View();
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    Randevu Appointment = new Randevu()
                    {
                        HastaTC = HttpContext.User.Identity.Name.ToString(),
                        RandevuTarihi = randevu.RandevuTarihi,
                        RandevuSaati = randevu.RandevuSaati,
                        RandevuAlmaTarihi = DateTime.Now,
                        Poliklinik = randevu.Poliklinik,
                        Hekim = randevu.Hekim,
                        Il = randevu.Il,
                        Ilce = randevu.Ilce
                    };

                    var addAppointment = _databaseContext.Entry(Appointment);
                    addAppointment.State = EntityState.Added;
                    _databaseContext.SaveChanges();
                }

                TempData["appointmentSuccess"] = "Randevunuz başarılı bir şekilde alınmıştır";

                return RedirectToAction("Index", "Home");
            }
        }
        public JsonResult GetDiscrit(int CityId)
        {
            _databaseContext.Configuration.ProxyCreationEnabled = false;
            List<Ilce> DiscritList = _databaseContext.IlceTable.Where(m => m.SehirId == CityId).ToList();
            return Json(DiscritList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDoctor(int BranchId)
        {
            _databaseContext.Configuration.ProxyCreationEnabled = false;
            List<Hekim> DoctorList = _databaseContext.HekimTable.Where(m => m.Brans == BranchId).ToList();
            return Json(DoctorList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AppointmentDelete(int id)
        {
            var appointment = _databaseContext.RandevuTable.Find(id);
            var deletetAppointment = _databaseContext.Entry(appointment);
            deletetAppointment.State = EntityState.Deleted;
            _databaseContext.SaveChanges();

            return RedirectToAction("Appointments", "Appointment");
        }
        public ActionResult Appointments()
        {
            DateTime randevuTarih = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            DatabaseContext context = new DatabaseContext();
            List<Randevu> randevular = context.RandevuTable.Where(m => m.HastaTC == HttpContext.User.Identity.Name && m.RandevuTarihi >= randevuTarih).ToList();
            return View(randevular);
        }
    }
}