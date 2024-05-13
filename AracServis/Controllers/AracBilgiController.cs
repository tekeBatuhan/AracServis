using AracServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracServis.Controllers
{
    public class AracBilgiController : Controller
    {
        public static int musteriId = 0;

        public ActionResult Index()
        {
            AracServisContext db = new AracServisContext();
            List<AracBilgi> aracBilgi = new List<AracBilgi>();

            if (musteriId == 0)
            {
                aracBilgi = db.AracBilgi.ToList();
            }
            else
            {
                aracBilgi = db.AracBilgi.Where(a => a.musteriID == musteriId).ToList();
            }

            return View(aracBilgi);
        }

        [HttpGet]
        public ActionResult MusteriAracIndex(int id)
        {
            musteriId = id;
            return RedirectToAction("Index", "AracBilgi");
        }

        [HttpGet]
        public ActionResult EkleIndex(int id)
        {
            AracServisContext db = new AracServisContext();
            List<SelectListItem> musteri = (from i in db.Musteri.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.ad,
                                                Value =i .musteriID.ToString()
                                            }).ToList();
            ViewBag.mst = musteri;
            AracBilgi aracBilgi = new AracBilgi();
            aracBilgi.musteriID = id;
            return View(aracBilgi);
        }



        [HttpPost]
        public ActionResult Ekle(AracBilgi a)
        {
     
            AracServisContext db = new AracServisContext();
            db.AracBilgi.Add(a);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Araç Bilgi başarıyla silindi.";
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            AracServisContext db = new AracServisContext();
            var arac = db.AracBilgi.Find(id);

            bool isIDUsedElsewhere = CheckIfIDIsUsedElsewhere(id);

            if (isIDUsedElsewhere)
            {
                TempData["ErrorMessage"] = "Bu ID Araç İşlem sayfasında kullanılıyor, silme işlemi iptal edildi.";
            }
            else
            {
                db.AracBilgi.Remove(arac);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Araç Bilgi başarıyla silindi.";
            }

            return RedirectToAction("Index", "Musteri");
        }
        public bool CheckIfIDIsUsedElsewhere(int id)
        {
            AracServisContext db = new AracServisContext();
            return db.Islem.Any(x => x.aracBilgiID == id);
        }

        public ActionResult Duzenle(int id)
        {
            AracServisContext db = new AracServisContext();
            var aracBilgi = db.AracBilgi.Find(id);
            List<SelectListItem> musteri = (from i in db.Musteri.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.ad,
                                                Value = i.musteriID.ToString()
                                            }).ToList();
            ViewBag.mst = musteri;
            return View("Duzenle", aracBilgi);
        }

        [HttpPost]
        public ActionResult Guncelle(AracBilgi ab)
        {
            AracServisContext db = new AracServisContext();
            var aracBilgi = db.AracBilgi.Find(ab.aracBilgiID);
            aracBilgi.marka = ab.marka;
            aracBilgi.model = ab.model;
            aracBilgi.sasiNo = ab.sasiNo;
            aracBilgi.plaka = ab.plaka;
            aracBilgi.musteriID = ab.musteriID;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Araç bilgisi başarıyla düzenlendi.";
            return RedirectToAction("Index");
        }

    }
}