using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class ServisBakim : BaseModel
    {
        public string Aciklama { get; set; }
        public Urun Urun { get; set; }
        public int UrunId { get; set; }
        public DateTime BakimTarihi { get; set; }
        public DateTime sonrakiBakimTarihi { get; set; }
        public Bayi Bayi { get; set; }
        public int BayiId { get; set; }
        public string SiparisNo { get; set; }
        public decimal BakimMasrafi { get; set; }
    }
}