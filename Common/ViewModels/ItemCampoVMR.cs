using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public class ItemCampoVMR
    {
        public long id { get; set; }
        public string etiqueta { get; set; }
        public string validacionRegEx { get; set; }
        public string textAyuda { get; set; }
        public string textError { get; set; }
        public string tipo { get; set; }

        public List<ItemCampoCatalogoVMR> itemCampoCatalogoList { get; set; }
    }
}
