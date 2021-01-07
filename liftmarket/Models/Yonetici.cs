using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Yonetici : BaseModel
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
    }
}