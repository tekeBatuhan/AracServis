using AracServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracServis.Controllers
{
    public class KullaniciController : Controller
    {
        public ActionResult Index(Kullanici k)
        {
            AracServisContext db = new AracServisContext();
            var kullanicilar = db.Kullanici.ToList();
            return View(kullanicilar);
        }

        [HttpGet]
        public ActionResult KullaniciEkleIndex()
        {
            AracServisContext db = new AracServisContext();
            List<SelectListItem> unvan = (from i in db.Unvan.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.ad,
                                              Value = i.unvanID.ToString()
                                          }).ToList();
            ViewBag.Unvan = unvan;
            return View();
        }

        public ActionResult KullaniciEkle(Kullanici k)
        {
            AracServisContext db = new AracServisContext();
            
            db.Kullanici.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }


        public ActionResult Sil(int id)
        {
            AracServisContext db = new AracServisContext();
            var kullanici = db.Kullanici.Find(id);
            db.Kullanici.Remove(kullanici);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Kullanıcı başarıyla silindi.";
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            AracServisContext db = new AracServisContext();
            var kullanici = db.Kullanici.Find(id);
            List<SelectListItem> unvan = (from i in db.Unvan.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.ad,
                                              Value = i.unvanID.ToString()
                                          }).ToList();
            ViewBag.Unvan = unvan;
            TempData["SuccessMessage"] = "Kullanıcı başarıyla Düzenlendi.";
            return View("Duzenle", kullanici);
        }

        [HttpPost]
        public ActionResult Guncelle(Kullanici k)
        {
            AracServisContext db = new AracServisContext();
            var kullanici = db.Kullanici.Find(k.kullaniciID);
            kullanici.ad = k.ad;
            kullanici.soyad = k.soyad;
            kullanici.eposta = k.eposta;
            kullanici.sifre = k.sifre;
            kullanici.unvanID = k.unvanID;
            kullanici.telefon = k.telefon;
            kullanici.adres = k.adres;
            kullanici.cinsiyet = k.cinsiyet;
            kullanici.kimlikNo = k.kimlikNo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}