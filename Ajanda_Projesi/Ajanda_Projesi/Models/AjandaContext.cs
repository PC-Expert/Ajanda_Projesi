using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ajanda_Projesi.Models
{
    public class AjandaContext : DbContext
    {
        public AjandaContext() : base("name=AjandaContext") { }
        public virtual DbSet<Olay> Olays { get; set; }
    }
}