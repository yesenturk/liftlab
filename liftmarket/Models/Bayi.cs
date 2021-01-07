using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Bayi : BaseModel
    {
        public int SehirId { get; set; }
        public Sehir Sehir { get; set; }
        public Ilce Ilce { get; set; }
        public int IlceId { get; set; }
        public string BayiKod { get; set; }
        public string Sifre { get; set; }

    }
}