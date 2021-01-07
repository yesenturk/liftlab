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
    public class BayiController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        BaseRepository<Siparisler> siparisRepo = new BaseRepository<Siparisler>();
        BaseRepository<ServisBakim> bakimRepo = new BaseRepository<ServisBakim>();

        // GET: Bayi
        public ActionResult Index()
        {
            return View(db.Urun.ToList());
        }
        public ActionResult Basket()
        {
            ViewBag.Title = "Sepet";

            List<SepetUrun> SepettekiUrunler = new List<SepetUrun>();

            if (Session["SepettekiUrunler"] != null)
            {
                SepettekiUrunler = (List<SepetUrun>)Session["SepettekiUrunler"];
            }

            if (SepettekiUrunler.Count > 0)
            {
                return View("Basket", SepettekiUrunler);
            }
            else
            {
                return View("EmptyBasket");
            }
        }

        public ActionResult AddToBasket(int? id)
        {
            List<SepetUrun> SepettekiUrunler = new List<SepetUrun>();

            if (Session["SepettekiUrunler"] != null)
            {
                SepettekiUrunler = (List<SepetUrun>)Session["SepettekiUrunler"];
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepetUrun tempUrun = SepettekiUrunler.Find(x => x.UrunId == id);

            if (tempUrun != null)
            {
                tempUrun.Adet++;
                tempUrun.UrunToplamFiyati = tempUrun.UrunFiyati * tempUrun.Adet;
                
            }
            else
            {
                Urun urun = db.Urun.Find(id);
                SepetUrun sepetUrun = new SepetUrun();
                sepetUrun.UrunId = urun.Id;
                sepetUrun.UrunAdi = urun.UrunAdi;
                sepetUrun.UrunFiyati = urun.BayiFiyati;
                sepetUrun.BakimFiyati = urun.BakimFiyati;
                sepetUrun.BakimSure = urun.BakimSure;
                sepetUrun.Adet = 1;
                sepetUrun.UrunToplamFiyati = urun.BayiFiyati * sepetUrun.Adet;

                SepettekiUrunler.Add(sepetUrun);

            }
            Session["SepettekiUrunler"] = SepettekiUrunler;

            if (SepettekiUrunler.Count > 0)
            {
                return RedirectToAction("Basket", "Bayi");
            }
            else
            {
                return RedirectToAction("Basket", "Bayi");
            }
        }
        public ActionResult SiparisVer()
        {
            List<SepetUrun> SepettekiUrunler = new List<SepetUrun>();
            int BayiId = 0;
            if (Session["SepettekiUrunler"] != null)
            {
                SepettekiUrunler = (List<SepetUrun>)Session["SepettekiUrunler"];
            }

            if (Session["BayiId"] != null)
                BayiId = (int)Session["BayiId"];
            Random random = new Random();
            string spNo = "20210000" + random.Next(1000, 10000);
            DateTime today = DateTime.Now;
            foreach (var item in SepettekiUrunler)
            {

                Siparisler siparisler = new Siparisler();
                siparisler.BayiId = BayiId;
                siparisler.UrunId = item.UrunId;
                siparisler.SiparisTarihi = today;
                siparisler.ToplamTutar = item.UrunToplamFiyati;
                siparisler.OdemeYontemleriId = 2;
                siparisler.OnaylandiMi = false;
                siparisler.SiparisNo = spNo;
                siparisler.Adet = item.Adet;

                siparisRepo.Add(siparisler);

                ServisBakim servisBakim = new ServisBakim();
                servisBakim.BayiId = BayiId;
                servisBakim.BakimMasrafi = item.BakimFiyati;
                servisBakim.sonrakiBakimTarihi = today.AddDays(item.BakimSure);
                servisBakim.BakimTarihi = today;
                servisBakim.SiparisNo = spNo;
                servisBakim.UrunId = item.UrunId;

                bakimRepo.Add(servisBakim);
            }
            Session.Remove("SepettekiUrunler");
            return RedirectToAction("Index");
        }
        public ActionResult Siparisler()
        {
            int BayiId = 0;
            if (Session["BayiId"] != null)
                BayiId = (int)Session["BayiId"];

            var mymodel = (from s in siparisRepo.Query<Siparisler>()
                           join
                           u in siparisRepo.Query<Urun>() on s.UrunId equals u.Id
                           //join
                           //b in siparisRepo.Query<ServisBakim>() on u.Id equals b.UrunId
                           where (s.BayiId == BayiId) && s.IsDeleted == false
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
    };
            //mymodel.Reverse();
            return View(orders);
        }
        public ActionResult OrderDetail(string SiparisNo)
        {
            int BayiId = 0;
            if (Session["BayiId"] != null)
                BayiId = (int)Session["BayiId"];

            var mymodel = (from s in siparisRepo.Query<Siparisler>()
                           join
                           u in siparisRepo.Query<Urun>() on s.UrunId equals u.Id
                           join
                           b in siparisRepo.Query<ServisBakim>() on u.Id equals b.UrunId
                           where (s.BayiId == BayiId && s.IsDeleted == false && s.SiparisNo == SiparisNo && b.SiparisNo == SiparisNo)
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
                               OnayDurumu = s.OnaylandiMi == true ? "Siparişiniz onaylandı" : "Siparişiniz onay bekliyor"
                           }).Distinct().ToList();

            //mymodel.Reverse();
            return View(mymodel);
        }

        // GET: Bayi/Edit/5
        public ActionResult UrunAdetGuncelle(int urunAdet, int urunId)
        {
            if (urunAdet == 0)
            {
                SepettenCikarUrunu(urunId);
            }
            List<SepetUrun> SepettekiUrunler = new List<SepetUrun>();

            if (Session["SepettekiUrunler"] != null)
            {
                SepettekiUrunler = (List<SepetUrun>)Session["SepettekiUrunler"];
            }

            if (SepettekiUrunler.Count > 0)
            {
                SepetUrun tempUrun = SepettekiUrunler.Find(x => x.UrunId == urunId);
                tempUrun.Adet = urunAdet;
                tempUrun.UrunToplamFiyati = urunAdet * tempUrun.UrunFiyati;

                Session["SepettekiUrunler"] = SepettekiUrunler;
                return RedirectToAction("Basket", "Bayi");
            }
            else
            {
                return RedirectToAction("Basket", "Bayi");
            }
        }

        public ActionResult SepettenCikarUrunu(int urunId)
        {
            List<SepetUrun> SepettekiUrunler = new List<SepetUrun>();

            if (Session["SepettekiUrunler"] != null)
            {
                SepettekiUrunler = (List<SepetUrun>)Session["SepettekiUrunler"];
            }

            if (SepettekiUrunler.Count > 0)
            {
                SepetUrun tempUrun = SepettekiUrunler.Find(x => x.UrunId == urunId);
                SepettekiUrunler.Remove(tempUrun);

                Session["SepettekiUrunler"] = SepettekiUrunler;
                return RedirectToAction("Basket", "Bayi");
            }
            else
            {
                return RedirectToAction("Basket", "Bayi");
            }
        }

        //// GET: Bayi/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Urun urun = db.Urun.Find(id);
        //    if (urun == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(urun);
        //}

        //// GET: Bayi/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Bayi/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,UrunAdi,UrunFiyati,UretimSure,BakimSure,CreateDate,IsDeleted,DeleteDate,UpdateDate")] Urun urun)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Urun.Add(urun);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(urun);
        //}

        //// GET: Bayi/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Urun urun = db.Urun.Find(id);
        //    if (urun == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(urun);
        //}

        //// POST: Bayi/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,UrunAdi,UrunFiyati,UretimSure,BakimSure,CreateDate,IsDeleted,DeleteDate,UpdateDate")] Urun urun)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(urun).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(urun);
        //}

        //// GET: Bayi/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Urun urun = db.Urun.Find(id);
        //    if (urun == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(urun);
        //}

        //// POST: Bayi/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Urun urun = db.Urun.Find(id);
        //    db.Urun.Remove(urun);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
