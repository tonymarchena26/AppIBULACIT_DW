using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Infracciones
    {
        public int Codigo { get; set; }
        public int Cedula { get; set; }
        public string TipoInfraccion { get; set; }
        public int Monto { get; set; }
        public string Descripcion { get; set; }
    }
}