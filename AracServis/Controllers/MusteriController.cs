using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AracServis.Models;

namespace AracServis.Controllers
{
    public class MusteriController : Controller
    {

        public ActionResult Index()
        {
            AracServisContext db = new AracServisContext();
            List<Musteri> musteriler = db.Musteri.ToList();
            return View(musteriler);
        }

        [HttpGet]
        public ActionResult MusteriEkleIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriEkle(Musteri m)
        {
            AracServisContext db = new AracServisContext();
            db.Musteri.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            AracServisContext db = new AracServisContext();
            var musteri = db.Musteri.Find(id);

            bool isIDUsedElsewhere = CheckIfIDIsUsedElsewhere(id);

            if (isIDUsedElsewhere)
            {
                TempData["ErrorMessage"] = "Bu ID Araç Bilgi sayfasında kullanılıyor, silme işlemi iptal edildi.";
            }
            else
            {
                db.Musteri.Remove(musteri);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Müşteri başarıyla silindi.";
            }

            return RedirectToAction("Index");
        }

        public bool CheckIfIDIsUsedElsewhere(int id)
        {
            AracServisContext db = new AracServisContext();
            return db.AracBilgi.Any(x => x.musteriID == id);
        }

        public ActionResult Duzenle(int id)
        {
            AracServisContext db = new AracServisContext();
            var musteri = db.Musteri.Find(id);
            return View("Duzenle", musteri);
        }

        [HttpPost]
        public ActionResult Guncelle(Musteri m)
        {
            AracServisContext db = new AracServisContext();
            var musteri = db.Musteri.Find(m.musteriID);
            musteri.ad = m.ad;
            musteri.soyad = m.soyad;
            musteri.telefon = m.telefon;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Müşteri bilgisi başarıyla düzenlendi.";
            return RedirectToAction("Index");
        }
    }
}