using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RandevuSistemi.Models;
using RandevuSistemi.Models.Tables;
using RandevuSistemi.ViewModels;

namespace RandevuSistemi.Controllers
{
    public class AdminController : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();
        public ActionResult Index()
        {
            int vatandas = _databaseContext.VatandasTable.Count();
            int hekim = _databaseContext.HekimTable.Count();
            int randevu = _databaseContext.RandevuTable.Count();
            int poliklinik = _databaseContext.PoliklinikTable.Count();

            ViewBag.UserTotal = vatandas;
            ViewBag.DoctorTotal = hekim;
            ViewBag.AppointmentTotal = randevu;
            ViewBag.PoliclinicTotal = poliklinik;

            return View();
        }
        public ActionResult UsersData()
        {
            var vatandaslar = _databaseContext.VatandasTable.ToList();
            return View(vatandaslar);
        }

        public ActionResult UserDataUpdate(string id)
        {
            var vatandas = _databaseContext.VatandasTable.Find(id);
            return View("UserDataUpdate", vatandas);
        }

        [HttpPost]
        public ActionResult UserDataUpdate(Vatandas vatandas)
        {

            var vatandasUpdate = _databaseContext.VatandasTable.Find(vatandas.TC);
            vatandasUpdate.Ad = vatandas.Ad;
            vatandasUpdate.Soyad = vatandas.Soyad;
            vatandasUpdate.DogumTarihi = vatandas.DogumTarihi;
            vatandasUpdate.Cinsiyet = vatandas.Cinsiyet;
            vatandasUpdate.Telefon = vatandas.Telefon;
            vatandasUpdate.EMail = vatandas.EMail;
            _databaseContext.SaveChanges();

            return RedirectToAction("UsersData", "Admin");
        }
        public ActionResult UserDelete(string id)
        {
            var vatandas = _databaseContext.VatandasTable.Find(id);
            var vatandasRandevu = _databaseContext.RandevuTable.Where(m => m.HastaTC == id).ToList();

            foreach (var item in vatandasRandevu)
            {
                var randevuDelete = _databaseContext.Entry(item);
                randevuDelete.State = EntityState.Deleted;
            }

            var vatandasDelete = _databaseContext.Entry(vatandas);
            vatandasDelete.State = EntityState.Deleted;

            _databaseContext.SaveChanges();

            return RedirectToAction("UsersData", "Admin");
        }
        public ActionResult DoctorsData()
        {
            var hekimler = _databaseContext.HekimTable.ToList();
            return View(hekimler);
        }

        public ActionResult DoctorAdd()
        {
            List<Poliklinik> Poliklinikler = _databaseContext.PoliklinikTable.ToList();
            ViewBag.Poliklinikler = new SelectList(Poliklinikler, "Id", "PoliklinikAdi");

            return View();
        }

        [HttpPost]
        public ActionResult DoctorAdd(Hekim hekim)
        {
            var addHekim = _databaseContext.Entry(hekim);
            addHekim.State = EntityState.Added;

            _databaseContext.SaveChanges();

            return RedirectToAction("DoctorsData", "Admin");
        }
        public ActionResult DoctorDataUpdate(int id)
        {
            List<Poliklinik> Poliklinikler = _databaseContext.PoliklinikTable.ToList();
            ViewBag.Poliklinikler = new SelectList(Poliklinikler, "Id", "PoliklinikAdi");

            var doktor = _databaseContext.HekimTable.Find(id);
            return View("DoctorDataUpdate", doktor);
        }

        [HttpPost]
        public ActionResult DoctorDataUpdate(Hekim hekim)
        {
            //List<Poliklinik> Poliklinikler = _databaseContext.PoliklinikTable.ToList();
            //ViewBag.Poliklinikler = new SelectList(Poliklinikler, "Id", "PoliklinikAdi");

            var doktorUpdate = _databaseContext.HekimTable.Find(hekim.Id);
            doktorUpdate.Isim = hekim.Isim;
            doktorUpdate.Brans = hekim.Brans;
            _databaseContext.SaveChanges();

            return RedirectToAction("DoctorsData", "Admin");
        }
        public ActionResult DoctorDelete(int id)
        {
            var doktor = _databaseContext.HekimTable.Find(id);
            var doktorDelete = _databaseContext.Entry(doktor);
            doktorDelete.State = EntityState.Deleted;

            _databaseContext.SaveChanges();

            return RedirectToAction("DoctorsData", "Admin");
        }

        public ActionResult AppointmentsData()
        {
            var randevular = _databaseContext.RandevuTable.ToList();
            return View(randevular);
        }

        public ActionResult AppointmentDelete(int id)
        {
            var randevu = _databaseContext.RandevuTable.Find(id);
            var deleteRandevu = _databaseContext.Entry(randevu);
            deleteRandevu.State = EntityState.Deleted;

            _databaseContext.SaveChanges();

            return RedirectToAction("AppointmentsData", "Admin");
        }
        public ActionResult PoliclinicData()
        {
            var poliklinikler = _databaseContext.PoliklinikTable.ToList();
            return View(poliklinikler);
        }
        public ActionResult PoliclinicAdd()
        {
            List<Poliklinik> Poliklinikler = _databaseContext.PoliklinikTable.ToList();
            ViewBag.Poliklinikler = new SelectList(Poliklinikler, "Id", "PoliklinikAdi");

            return View();
        }

        [HttpPost]
        public ActionResult PoliclinicAdd(Poliklinik poliklinik)
        {
            var addPoliklinik = _databaseContext.Entry(poliklinik);
            addPoliklinik.State = EntityState.Added;

            _databaseContext.SaveChanges();

            return RedirectToAction("PoliclinicData", "Admin");
        }
        public ActionResult PoliclinicUpdate(int id)
        {
            var poliklinik = _databaseContext.PoliklinikTable.Find(id);
            return View("PoliclinicUpdate", poliklinik);
        }


        [HttpPost]
        public ActionResult PoliclinicUpdate(Poliklinik poliklinik)
        {
            var updatePoliklinik = _databaseContext.PoliklinikTable.Find(poliklinik.Id);
            updatePoliklinik.PoliklinikAdi = poliklinik.PoliklinikAdi;

            _databaseContext.SaveChanges();

            return RedirectToAction("PoliclinicData","Admin");
        }
        public ActionResult PoliclinicDelete(int id)
        {
            var poliklinik = _databaseContext.PoliklinikTable.Find(id);
            var deletePoliklinik = _databaseContext.Entry(poliklinik);

            deletePoliklinik.State = EntityState.Deleted;

            _databaseContext.SaveChanges();

            return RedirectToAction("PoliclinicData", "Admin");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult AdminPasswordUpdate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminPasswordUpdate(ComparePasswordViewModel vatandas)
        {
            var parola = _databaseContext.VatandasTable.Where(m => m.TC == HttpContext.User.Identity.Name).ToList();
            bool kontrol = false;

            foreach (var item in parola)
            {
                if (vatandas.mevcutParola == item.Parola)
                {
                    kontrol = true;
                }
                else
                {
                    kontrol = false;
                }
            }

            if (kontrol == true)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    Vatandas updatePassword = context.VatandasTable.Find(HttpContext.User.Identity.Name);
                    updatePassword.Parola = vatandas.Parola;
                    context.SaveChanges();
                }
                return RedirectToAction("Settings", "Admin");
            }
            else
            {
                ViewBag.PasswordError = "Mevcut Parola hatalı lütfen kontrol edin";
                return View();
            }
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Vatandas vatandas)
        {
            using (var context = new DatabaseContext())
            {
                Vatandas admin = new Vatandas()
                {
                    TC = vatandas.TC,
                    Ad = vatandas.Ad,
                    Soyad = vatandas.Soyad,
                    DogumTarihi = vatandas.DogumTarihi,
                    Cinsiyet = vatandas.Cinsiyet,
                    EMail = vatandas.EMail,
                    Telefon = vatandas.Telefon,
                    GuvenlikSorusu = vatandas.GuvenlikSorusu,
                    GuvenlikCevabi = vatandas.GuvenlikCevabi,
                    Parola = vatandas.Parola,
                    Role = "admin"
                };

                var addAdmin = _databaseContext.Entry(admin);
                addAdmin.State = EntityState.Added;
                _databaseContext.SaveChanges();
            }

            return RedirectToAction("UsersData", "Admin");
        }

        public ActionResult Info()
        {
            return View();
        }
    }
}