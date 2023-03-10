using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class TelefonoContactoVMR
    {
        public long id { get; set; }
        public string numero { get; set; }
        public long telefonoOperadoraId { get; set; }
        public long proveedorId { get; set; }
    }
}
