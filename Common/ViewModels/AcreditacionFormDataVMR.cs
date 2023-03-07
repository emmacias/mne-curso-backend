using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class AcreditacionFormDataVMR
    {
        public List<FormaPagoVMR> formaPagoList { get; set; }

        public AcreditacionFormDataVMR()
        {
            formaPagoList = new List<FormaPagoVMR>();
        }
    }
}
