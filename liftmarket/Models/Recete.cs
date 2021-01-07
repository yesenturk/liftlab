using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Recete : BaseModel
    {
        public List<Hammadde> Hammadde { get; set; }
        public List<int> HammaddeId { get; set; }
        public Urun Urun { get; set; }
        public int UrunId { get; set; }
    }
}