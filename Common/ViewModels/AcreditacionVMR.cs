using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class AcreditacionVMR
    {
        public long id { get; set; }
        public long puntoVentaId { get; set; }
        public decimal valor { get; set; }
        public Nullable<System.DateTime> fechaAcreditacion { get; set; }
        public long formaPagoId { get; set; }

        public FormaPagoVMR formaPago { get; set; }
    }
}
