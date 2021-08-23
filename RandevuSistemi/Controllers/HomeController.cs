using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RandevuSistemi.Models;
using RandevuSistemi.Models.Tables;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;
using RandevuSistemi.ViewModels;

namespace RandevuSistemi.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Vatandas vatandasBilgileri)
        {
            var kontrol = _databaseContext.VatandasTable.FirstOrDefault(m => m.TC == vatandasBilgileri.TC && m.Parola == vatandasBilgileri.Parola);
            var vatandas = _databaseContext.VatandasTable.Where(m => m.TC == vatandasBilgileri.TC).ToList();

            if (kontrol != null && kontrol.Role == "user")
            {
                FormsAuthentication.SetAuthCookie(vatandasBilgileri.TC, false);
                foreach (var item in vatandas)
                {
                    Session["UserName"] = item.Ad + " " + item.Soyad;
                }
                return RedirectToAction("Index", "Home");

            }
            else if (kontrol != null && kontrol.Role == "admin")
            {
                FormsAuthentication.SetAuthCookie(vatandasBilgileri.TC, false);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ResultMessage = "TC kimlik No/Kullanıcı adı veya parola hatalı";
                return View();
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("Register", new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel vatandas)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            bool durum = false;

            var kontrol = _databaseContext.VatandasTable.ToList();

            foreach (var item in kontrol)
            {
                if (vatandas.TC == item.TC)
                {
                    durum = true;
                    break;
                }
                durum = false;
            }

            if (durum != true)
            {
                using (var context = new DatabaseContext())
                {
                    Vatandas user = new Vatandas() 
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
                        Role = "user"
                    };
                    var addVatandas = _databaseContext.Entry(user);
                    addVatandas.State = EntityState.Added;
                    _databaseContext.SaveChanges();
                }
                
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.ResultMessage = "E Randevu Sistemine zaten üyesiniz!";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}