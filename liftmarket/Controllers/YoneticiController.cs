using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using liftmarket.Models;
using liftmarket.Models.Repository;

namespace liftmarket.Controllers
{
    public class YoneticiController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        BaseRepository<Urun> urunRepo = new BaseRepository<Urun>();
        BaseRepository<Siparisler> siparisRepo = new BaseRepository<Siparisler>();

        // GET: Yonetici
        public ActionResult Index()
        {
            return View(db.Urun.ToList());
        }

        // GET: Yonetici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Yonetici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UrunAdi,BayiFiyati,MusteriFiyati,BakimFiyati,UretimSure,BakimSure,KarDurumu,CreateDate,IsDeleted,DeleteDate,UpdateDate")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                urunRepo.Add(urun);

                return RedirectToAction("Index");
            }

            return View(urun);
        }

        // GET: Yonetici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Yonetici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UrunAdi,BayiFiyati,MusteriFiyati,BakimFiyati,UretimSure,BakimSure,KarDurumu,CreateDate,IsDeleted,DeleteDate,UpdateDate")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(urun);
        }

        // GET: Yonetici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Yonetici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);
            urun.IsDeleted = true;
            //db.Urun.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Siparisler()
        {
            var mymodel = (from s in siparisRepo.Query<Siparisler>()
                           join
                           u in siparisRepo.Query<Urun>() on s.UrunId equals u.Id
                           //join
                           //b in siparisRepo.Query<ServisBakim>() on u.Id equals b.UrunId
                           where s.IsDeleted == false
                           orderby s.CreateDate descending
                           select new SiparisDetay()
                           {
                               SiparisNo = s.SiparisNo,
                               UrunAdi = u.UrunAdi,
                               SiparisTarihi = s.SiparisTarihi,
                               Adet = s.Adet,
                               //SonBakimTarihi = b.BakimTarihi,
                               //SonrakiBakimTarihi = b.BakimTarihi,
                               BakimMasrafi = u.BakimFiyati,
                               BakimSuresi = u.BakimSure,
                               OnayDurumu = s.OnaylandiMi == true ? "Siparişiniz onaylandı" : "Siparişiniz onay bekliyor",
                               OnaylandiMi = s.OnaylandiMi,
                               ToplamTutar = s.ToplamTutar
                           }).Distinct().ToList();
            //    var newmodel= mymodel
            //    .GroupBy(p => p.SiparisNo,
            //             (k, c,d) => new SiparisDetay()
            //             {
            //                 SiparisNo = k,
            //                 Urunler = c.Select(cs => cs.Urun).ToList(),.
            //                 Adet=d.Sum(x => x.ad),
            //             };
            //}
            //            ).ToList();

            var orders =
    from h in mymodel
    group h by h.SiparisNo into r
    select new SiparisDetay()
    {
        SiparisNo = r.Key,
        Adet = r.Sum(x => x.Adet),
        ToplamTutar = r.Sum(x => x.ToplamTutar),
        OnaylandiMi = mymodel.Where(s => s.SiparisNo == r.Key).FirstOrDefault().OnaylandiMi
    };
            //mymodel.Reverse();
            return View(orders);
        }

        public ActionResult OrderDetail(string SiparisNo)
        {
            var mymodel = (from s in siparisRepo.Query<Siparisler>()
                           join
                           u in siparisRepo.Query<Urun>() on s.UrunId equals u.Id
                           join
                           b in siparisRepo.Query<ServisBakim>() on u.Id equals b.UrunId
                           where (s.IsDeleted == false && s.SiparisNo == SiparisNo && b.SiparisNo == SiparisNo)
                           orderby s.CreateDate descending
                           select new SiparisDetay()
                           {
                               SiparisNo = s.SiparisNo,
                               UrunAdi = u.UrunAdi,
                               SiparisTarihi = s.SiparisTarihi,
                               Adet = s.Adet,
                               SonBakimTarihi = b.BakimTarihi != null ? b.BakimTarihi : s.SiparisTarihi,
                               SonrakiBakimTarihi = b.sonrakiBakimTarihi,
                               BakimMasrafi = u.BakimFiyati,
                               BakimSuresi = u.BakimSure,
                               OnayDurumu = s.OnaylandiMi == true ? "Siparişiniz onaylandı" : "Siparişiniz onay bekliyor",
                               OnaylandiMi = s.OnaylandiMi
                           }).Distinct().ToList();

            //mymodel.Reverse();
            return View(mymodel);
        }
        public ActionResult Onayla(string SiparisNo)
        {
            List<Siparisler> siparisler = db.Siparisler.ToList();
            foreach (var item in siparisler)
            {
                if (item.SiparisNo == SiparisNo)
                    item.OnaylandiMi = true;
            }
            db.SaveChanges();
            return RedirectToAction("Siparisler", "yonetici");

        }
        public ActionResult OdemeBilgisiEkle(string SiparisNo)
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
