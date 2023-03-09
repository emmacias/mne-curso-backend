using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class ProveedorVMR
    {
        public long id { get; set; }
        public string nombre { get; set; }
        public Nullable<bool> bloqueado { get; set; }
        public string urlServicios { get; set; }
    }
}
