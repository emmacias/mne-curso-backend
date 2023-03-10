using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class ProveedorFormDataVMR
    {
        public List<TelefonoOperadoraVMR> telefonoOperadoraList { get; set; }

        public ProveedorFormDataVMR()
        {
            telefonoOperadoraList = new List<TelefonoOperadoraVMR>();
        }
    }
}
