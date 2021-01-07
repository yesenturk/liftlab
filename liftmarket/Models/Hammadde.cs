using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Hammadde : BaseModel
    {
        public string HammaddeAdi { get; set; }
        public int StokSayisi { get; set; }
        public int TedarikSuresi { get; set; }
    }
}