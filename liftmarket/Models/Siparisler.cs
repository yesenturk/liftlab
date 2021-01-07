using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Siparisler : BaseModel
    {
        public Bayi Bayi { get; set; }
        public int BayiId { get; set; }
        public Urun Urun { get; set; }
        public int UrunId { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public OdemeYontemleri OdemeYontemleri { get; set; }
        public int? OdemeYontemleriId { get; set; }
        public decimal ToplamTutar { get; set; }
        public Boolean OnaylandiMi { get; set; }
        public int Adet { get; set; }
        public string SiparisNo { get; set; }
    }
}