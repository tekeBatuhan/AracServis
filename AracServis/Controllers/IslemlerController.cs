using AracServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracServis.Controllers
{
    public class IslemlerController : Controller
    {
        public ActionResult Index()
        {
            AracServisContext db = new AracServisContext();
            List<IslemTur> islemler = db.IslemTur.ToList();
            return View(islemler);
        }

        [HttpGet]
        public ActionResult EkleIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IslemEkle(IslemTur it)
        {
            AracServisContext db = new AracServisContext();
            db.IslemTur.Add(it);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            AracServisContext db = new AracServisContext();
            var islemTur = db.IslemTur.Find(id);

            bool isIDUsedElsewhere = CheckIfIDIsUsedElsewhere(id);

            if (isIDUsedElsewhere)
            {
                TempData["ErrorMessage"] = "Bu ID başka bir yerde kullanılıyor, silme işlemi iptal edildi.";
            }
            else
            {
                db.IslemTur.Remove(islemTur);
                db.SaveChanges();
                TempData["SuccessMessage"] = "İşlem Türü başarıyla silindi.";
            }

            return RedirectToAction("Index");
        }

        public bool CheckIfIDIsUsedElsewhere(int id)
        {
            AracServisContext db = new AracServisContext();
            return db.Islem.Any(x =>x.islemTurID == id);
        }




        public ActionResult Duzenle(int id)
        {
            AracServisContext db = new AracServisContext();
            var islemTur = db.IslemTur.Find(id);
            return View("Duzenle", islemTur);
        }

        [HttpPost]
        public ActionResult Guncelle(IslemTur it)
        {
            AracServisContext db = new AracServisContext();
            var islemTur = db.IslemTur.Find(it.islemTurID);
            islemTur.ad = it.ad;
            islemTur.suresi = it.suresi;
            islemTur.fiyat = it.fiyat;
            islemTur.islemTurID = it.islemTurID;
            db.SaveChanges();
            TempData["SuccessMessage"] = "İşlem Türü başarıyla düzenlendi.";
            return RedirectToAction("Index");
        }

        public ActionResult MusteriIslemIndex(int id)
        {
            AracServisContext db = new AracServisContext();
            List<Islem> islem = db.Islem.Where(i => i.aracBilgiID == id).ToList();
            return View(islem);
        }

        [HttpGet]
        public ActionResult MusteriIslemEkleIndex(int id)
        {
            AracServisContext db = new AracServisContext();
            List<SelectListItem> ad = (from i in db.IslemTur.ToList()
                                       select new SelectListItem
                                       {
                                           Text = i.ad,
                                           Value = i.islemTurID.ToString()
                                       }).ToList();
            ViewBag.IslemTur = ad;
            Islem islem = new Islem();
            islem.aracBilgiID = id;
            return View(islem);
        }

        [HttpPost]
        public ActionResult MusteriIslemEkle(Islem i)
        {
            AracServisContext db = new AracServisContext();
            db.Islem.Add(i);
            db.SaveChanges();
            return RedirectToAction("MusteriIslemIndex", new { id = i.aracBilgiID });
        }

     


        public ActionResult MusteriIslemSil(int id)
        {
            AracServisContext db = new AracServisContext();
            var ıslem = db.Islem.Find(id);
            db.Islem.Remove(ıslem);
            db.SaveChanges();
            TempData["SuccessMessage"] = "İşlem Türü başarıyla düzenlendi.";
            return RedirectToAction("Index", "AracBilgi");
        }

        public ActionResult MusteriIslemDuzenle(int id)
        {
            AracServisContext db = new AracServisContext();
            var islem = db.Islem.Find(id);
            List<SelectListItem> islemTur = (from i in db.IslemTur.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.ad,
                                              Value = i.islemTurID.ToString()
                                          }).ToList();
            ViewBag.IslemTur = islemTur;
            return View("Duzenle", islem);

        }
        public ActionResult MusteriIslemGuncelle(Islem i)
        {
            AracServisContext db = new AracServisContext();
            var islem = db.Islem.Find(i.islemID);
            islem.islemTurID = i.islemTurID;
            islem.girisTarihi = i.girisTarihi;
            islem.cikisTarihi = i.cikisTarihi;
            islem.aciklama = i.aciklama;
            db.SaveChanges();
            TempData["SuccessMessage"] = "İşlem Türü başarıyla düzenlendi.";
            return RedirectToAction("Index");
        }


    }
}