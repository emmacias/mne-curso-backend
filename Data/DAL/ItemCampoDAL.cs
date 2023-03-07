using Common.ViewModels;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class ItemCampoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId">ryrjrru</param>
        public static List<ItemCampoVMR> ObtenerCamposPorItem(long itemId)
        {
            List<ItemCampoVMR> result = new List<ItemCampoVMR>();

            using (var db = DbConnection.Create()) {
                //result = db.ItemCampo.Where(ic => ic.habilitado && itemId == ic.itemId)
                //    .OrderBy(ic => ic.orden)
                //    .Select(ic => new ItemCampoVMR { 
                //        id = ic.id,
                //        etiqueta= ic.etiqueta,
                //        textAyuda = ic.textAyuda,
                //        validacionRegEx = ic.validacionRegEx,
                //        textError = ic.textError,
                //        tipo = ic.tipo,
                //        itemCampoCatalogoList = db.ItemCampoCatalogo.Where(icc => 
                //            icc.itemCampoId == ic.id 
                //            && icc.habilitado
                //        ).Select(icc => new ItemCampoCatalogoVMR { 
                //            id = icc.id,
                //            nombre = icc.nombre,
                //            valor = icc.valor
                //        }).ToList()
                //    }).ToList();

                result = db.Database.SqlQuery<ItemCampoVMR>(@"
                    exec ObtenerCamposPorItem @itemId
                ", 
                new SqlParameter[] { 
                    new SqlParameter("@itemId", itemId)
                }).ToList();

                foreach(var item in result)
                {
                    item.itemCampoCatalogoList = db.ItemCampoCatalogo.Where(icc => 
                                                    icc.itemCampoId == item.id
                                                    && icc.habilitado
                                                 ).Select(icc => new ItemCampoCatalogoVMR {
                                                     id = icc.id,
                                                     nombre = icc.nombre,
                                                     valor = icc.valor
                                                 }).ToList();
                }
            }

            return result;
        }
    }
}
