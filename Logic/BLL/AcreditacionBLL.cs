using Common.Util;
using Data.DAL;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BLL
{
    public class AcreditacionBLL
    {
        public static long Post(Acreditacion item)
        {
            return AcreditacionDAL.Post(item);
        }
    }
}
