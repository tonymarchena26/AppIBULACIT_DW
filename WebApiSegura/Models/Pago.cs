//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiSegura.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pago
    {
        public int Codigo { get; set; }
        public int CodigoServicio { get; set; }
        public int CodigoCuenta { get; set; }
        public int CodigoMoneda { get; set; }
        public System.DateTime FechaHora { get; set; }
        public decimal Monto { get; set; }
    
        public virtual Cuenta Cuenta { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual Servicio Servicio { get; set; }
    }
}
