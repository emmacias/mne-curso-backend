﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbConnection : DbContext
    {
        public DbConnection()
            : base("name=DbConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Acreditacion> Acreditacion { get; set; }
        public virtual DbSet<FormaPago> FormaPago { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemCampo> ItemCampo { get; set; }
        public virtual DbSet<ItemCampoCatalogo> ItemCampoCatalogo { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<PuntoVenta> PuntoVenta { get; set; }
        public virtual DbSet<Saldo> Saldo { get; set; }
        public virtual DbSet<TelefonoContacto> TelefonoContacto { get; set; }
        public virtual DbSet<TelefonoOperadora> TelefonoOperadora { get; set; }
        public virtual DbSet<TransaccionPago> TransaccionPago { get; set; }
    }
}
