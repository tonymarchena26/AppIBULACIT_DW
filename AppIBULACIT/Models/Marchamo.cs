using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Marchamo
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int MontoMarchamo { get; set; }
        public int CodigoUsuario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}