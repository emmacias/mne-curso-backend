using Common.ViewModels;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BLL
{
    public class ItemCampoBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static List<ItemCampoVMR> ObtenerCamposPorItem(long itemId)
        {
            return ItemCampoDAL.ObtenerCamposPorItem(itemId);
        }
    }
}
