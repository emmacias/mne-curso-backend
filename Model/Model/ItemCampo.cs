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
    
    public partial class ItemCampo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemCampo()
        {
            this.ItemCampoCatalogo = new HashSet<ItemCampoCatalogo>();
        }
    
        public long id { get; set; }
        public string etiqueta { get; set; }
        public string validacionRegEx { get; set; }
        public int orden { get; set; }
        public string textAyuda { get; set; }
        public string textError { get; set; }
        public bool habilitado { get; set; }
        public long itemId { get; set; }
        public string tipo { get; set; }
    
        public virtual Item Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemCampoCatalogo> ItemCampoCatalogo { get; set; }
    }
}
