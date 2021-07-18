using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Recarga
    {
        public int Codigo { get; set; }
        public string Propietario { get; set; }
        public int Telefono { get; set; }
        public decimal Monto { get; set; }
    }
}