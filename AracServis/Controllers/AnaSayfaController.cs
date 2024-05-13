using AracServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracServis.Controllers
{
    public class AnaSayfaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string eposta,string sifre)
        {
            AracServisContext db = new AracServisContext();
            Kullanici kullanici = db.Kullanici.Where(x => x.eposta == eposta && x.sifre == sifre).SingleOrDefault();
            if (kullanici == null)
            {
                ViewBag.sonuc = "Kullanıcı bulunamadı.";
                return View();
            }
            else
            {
                Session["Kullanici"] = kullanici;
                return RedirectToAction("Anasayfa");
            }

        }


        public ActionResult Anasayfa()
        {
            AracServisContext db = new AracServisContext();
            int kullaniciSayisi = db.Kullanici.Count(); 
            ViewBag.KullaniciSayisi = kullaniciSayisi;

            int musteriSayisi = db.Musteri.Count();
            ViewBag.musteriSayisi = musteriSayisi;

            int islemlerSayisi = db.IslemTur.Count();
            ViewBag.islemlerSayisi = islemlerSayisi;
            return View();
        }

    }
}