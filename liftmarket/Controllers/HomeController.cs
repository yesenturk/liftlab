using liftmarket.Models;
using liftmarket.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace liftmarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            Session.Remove("Yonetici");
            Session.Remove("Bayi");
            Session["Musteri"] = true;

            return View();
        }

        public ActionResult Hakkimizda()
        {
            ViewBag.Title = "Hakkımızda";

            return View("Hakkimizda");
        }

        public ActionResult Urunlerimiz()
        {
            ViewBag.Title = "Ürünlerimiz";

            return View("Urunlerimiz");
        }

        public ActionResult Iletisim()
        {
            ViewBag.Title = "İletişim";

            return View("Iletisim");
        }

        public ActionResult YoneticiGiris()
        {
            ViewBag.Title = "YoneticiGiriş";

            return View("YoneticiGiris");
        }

        public ActionResult BayiGiris()
        {
            ViewBag.Title = "BayiGiriş";

            return View("BayiGiris");
        }

        public ActionResult MusteriGiris()
        {
            ViewBag.Title = "MüşteriGiriş";

            return View("MusteriGiris");
        }

        [HttpPost]
        public ActionResult BayiLogin(string BayiKodu, string Sifre)
        {
            BaseRepository<Bayi> _repo = new BaseRepository<Bayi>();

            var repo = _repo.Query<Bayi>().Where(x => x.BayiKod == BayiKodu && x.Sifre == Sifre && !x.IsDeleted).FirstOrDefault();

            if (repo != null)
            {
                //Bayi _temp = new Bayi();
                //foreach (var item in repo)
                //{
                //    _temp.BayiKod = item.BayiKod;
                //    _temp.Id = item.Id;
                //    //_temp.LastName = item.LastName;
                //    //_temp.Dis = item.Dis;

                //}

                //SessionSet<Bayi>.Set(_temp, "Bayilogin");
                Session.Remove("Yonetici");
                Session.Remove("Musteri");
                Session["Bayi"] = true;

                Session["BayiId"] = repo.Id;
                return RedirectToAction("Index", "Bayi");
            }
            else
            {
                ModelState.AddModelError("", "Hatalı Giriş Yaptınız");
                return View("BayiGiris");
            }
        }

        [HttpPost]
        public ActionResult YoneticiLogin(string KullaniciAdi, string Sifre)
        {
            BaseRepository<Yonetici> _repo = new BaseRepository<Yonetici>();

            var repo = _repo.Query<Yonetici>().Where(x => x.KullaniciAdi == KullaniciAdi && x.Sifre == Sifre && !x.IsDeleted).FirstOrDefault();

            if (repo != null)
            {
                //Yonetici _temp = new Yonetici();
                //foreach (var item in repo)
                //{
                //    _temp.YoneticiKod = item.YoneticiKod;
                //    _temp.Id = item.Id;
                //    //_temp.LastName = item.LastName;
                //    //_temp.Dis = item.Dis;

                //}

                //SessionSet<Yonetici>.Set(_temp, "Yoneticilogin");
                Session.Remove("Bayi");
                Session.Remove("Musteri");
                Session["Yonetici"] = true;

                Session["YoneticiId"] = repo.Id;

                return RedirectToAction("Index", "Yonetici");
            }
            else
            {
                ModelState.AddModelError("", "Hatalı Giriş Yaptınız");
                return View("YoneticiGiris");
            }


        }

    }

}

