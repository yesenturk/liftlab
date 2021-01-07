using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Urun : BaseModel
    {
        public string UrunAdi { get; set; }
        public decimal BayiFiyati { get; set; }
        public decimal MusteriFiyati { get; set; }
        public decimal BakimFiyati { get; set; }
        public int UretimSure { get; set; }
        public int BakimSure { get; set; }
        public decimal KarDurumu { get; set; }
    }
}