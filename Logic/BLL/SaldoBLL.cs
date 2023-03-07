using Common.Util;
using Common.ViewModels;
using Data.DAL;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BLL
{
    public class SaldoBLL
    {
        public static long Post(Saldo item)
        {
            return SaldoDAL.Post(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdvId"></param>
        /// <returns></returns>
        public static SaldoVMR GetSaldoDePDV(long pdvId)
        {
            return SaldoDAL.GetSaldoDePDV(pdvId);
        }
    }
}
