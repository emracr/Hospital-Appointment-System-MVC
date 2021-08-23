using RandevuSistemi.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RandevuSistemi.Models;
using RandevuSistemi.ViewModels;

namespace RandevuSistemi.Controllers
{
    public class UserController : Controller
    {
        DatabaseContext _databaseContext = new DatabaseContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PasswordReset(Vatandas vatandas)
        {
            var kontrol = _databaseContext.VatandasTable.FirstOrDefault(m => m.TC == vatandas.TC);
            var user = _databaseContext.VatandasTable.Where(m => m.TC == vatandas.TC).ToList();

            var userTC = "";
            var userGuvenlikSorusu = "";

            foreach (var item in user)
            {
                userTC = item.TC;
                userGuvenlikSorusu = item.GuvenlikSorusu;
            }

            if (kontrol != null)
            {
                TempData["userTCKN"] = userTC;
                TempData["userSecurityQuestion"] = userGuvenlikSorusu;
                return RedirectToAction("SecurityQuestion", "User");
            }
            else
            {
                ViewBag.UserNotFound = "Malesef sistemde kaydınız bulunmamaktadır.";
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SecurityQuestion()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SecurityQuestion(Vatandas vatandas)
        {
            var kontrol = _databaseContext.VatandasTable.FirstOrDefault(m => m.TC == vatandas.TC && m.GuvenlikCevabi == vatandas.GuvenlikCevabi);

            var user = _databaseContext.VatandasTable.Where(m => m.TC == vatandas.TC).ToList();

            var userTC = "";
            var userGuvenlikSorusu = "";

            foreach (var item in user)
            {
                userTC = item.TC;
                userGuvenlikSorusu = item.GuvenlikSorusu;
            }

            TempData["userTCKN"] = userTC;
            TempData["userSecurityQuestion"] = userGuvenlikSorusu;

            if (kontrol != null)
            {
                return RedirectToAction("NewPassword", "User");
            }

            else
            {
                ViewBag.SecurityQuestionResult = "Güvenlik sorusunun cevabı hatalı";
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult NewPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult NewPassword(Vatandas vatandas)
        {

            using (DatabaseContext context = new DatabaseContext())
            {
                Vatandas updatePassword = context.VatandasTable.Find(vatandas.TC);
                updatePassword.Parola = vatandas.Parola;
                context.SaveChanges();
            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult Profil()
        {
            return View();
        }
        public ActionResult MyPersonelInformation()
        {
            var vatandas = _databaseContext.VatandasTable.Find(HttpContext.User.Identity.Name);
            return View("MyPersonelInformation", vatandas);
        }
        public ActionResult MyPersonelInformationUpdate(Vatandas bilgiler)
        {
            var vatandas = _databaseContext.VatandasTable.Find(HttpContext.User.Identity.Name);
            vatandas.Ad = bilgiler.Ad;
            vatandas.Soyad = bilgiler.Soyad;
            vatandas.DogumTarihi = bilgiler.DogumTarihi;
            vatandas.Cinsiyet = bilgiler.Cinsiyet;
            _databaseContext.SaveChanges();

            Session["UserName"] = bilgiler.Ad + " " + bilgiler.Soyad;

            return RedirectToAction("Profil", "User");
        }
        public ActionResult MyContactDetails()
        {
            var vatandas = _databaseContext.VatandasTable.Find(HttpContext.User.Identity.Name);
            return View("MyContactDetails", vatandas);
        }

        public ActionResult MyContactDetailsUpdate(Vatandas  bilgiler)
        {
            var vatandas = _databaseContext.VatandasTable.Find(HttpContext.User.Identity.Name);
            vatandas.Telefon = bilgiler.Telefon;
            vatandas.EMail = bilgiler.EMail;
            _databaseContext.SaveChanges();

            return RedirectToAction("Profil", "User");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ComparePasswordViewModel vatandas)
        {
            var parola = _databaseContext.VatandasTable.Where(m => m.TC == HttpContext.User.Identity.Name).ToList();
            bool kontrol = false;

            foreach (var item in parola)
            {
                if (vatandas.mevcutParola == item.Parola)
                {
                    kontrol = true;
                    break;
                }

                kontrol = false;
            }

            if (kontrol == true)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    Vatandas updatePassword = context.VatandasTable.Find(HttpContext.User.Identity.Name);
                    updatePassword.Parola = vatandas.Parola;
                    context.SaveChanges();
                }
                return RedirectToAction("Profil", "User");
            }
            else
            {
                ViewBag.PasswordError = "Mevcut Parola hatalı lütfen kontrol edin";
                return View();
            }
        }
    }
}