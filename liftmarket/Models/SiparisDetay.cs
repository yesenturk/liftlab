using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class SiparisDetay : BaseModel
    {
        public string SiparisNo { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public Bayi Bayi { get; set; }
        public int BayiId { get; set; }
        public Urun Urun { get; set; }
        public List<Urun> Urunler { get; set; }

        public int UrunId { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public DateTime SonBakimTarihi { get; set; }
        public DateTime SonrakiBakimTarihi { get; set; }
        public decimal BakimMasrafi { get; set; }
        public int BakimSuresi { get; set; }
        public OdemeYontemleri OdemeYontemleri { get; set; }
        public int? OdemeYontemleriId { get; set; }
        public decimal ToplamTutar { get; set; }
        public string OnayDurumu { get; set; }
        public Boolean OnaylandiMi { get; set; }
    }
}