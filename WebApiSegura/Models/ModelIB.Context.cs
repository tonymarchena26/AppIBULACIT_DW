﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiSegura.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class INTERNET_BANKIN_ULACIT_DWEntities1 : DbContext
    {
        public INTERNET_BANKIN_ULACIT_DWEntities1()
            : base("name=INTERNET_BANKIN_ULACIT_DWEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Estadistica> Estadisticas { get; set; }
        public virtual DbSet<Moneda> Monedas { get; set; }
        public virtual DbSet<Pago> Pagoes { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<Sesion> Sesions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Transferencia> Transferencias { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
