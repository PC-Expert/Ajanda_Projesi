using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajanda_Projesi.Models
{
    public class Olay
    {
        public int id { get; set; }

        public string metin { get; set; }

        public DateTime baslangic { get; set; }

        public DateTime bitis { get; set; }
    }
}