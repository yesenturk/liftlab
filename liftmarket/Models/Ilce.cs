using liftmarket.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace liftmarket.Models
{
    public class Ilce
    {
        [Key]
        public int Id { get; set; }
        public int SehirId { get; set; }
        public Sehir Sehir { get; set; }
        public string IlceAdi { get; set; }
    }
}