using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class SepetUrun : BaseModel
    {
        public string UrunAdi { get; set; }
        public int UrunId { get; set; }
        public decimal UrunFiyati { get; set; }
        public decimal BakimFiyati { get; set; }
        public decimal UrunToplamFiyati { get; set; }
        public int UretimSure { get; set; }
        public int BakimSure { get; set; }
        public int Adet { get; set; }


    }
}