//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TransaccionPago
    {
        public long id { get; set; }
        public System.DateTime fecha { get; set; }
        public int estado { get; set; }
        public long itemId { get; set; }
        public long puntoVentaId { get; set; }
        public string exception { get; set; }
        public decimal valor { get; set; }
        public string nroTransaccionProveedor { get; set; }
    }
}
