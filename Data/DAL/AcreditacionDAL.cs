using Common.Util;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class AcreditacionDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static long Post(Acreditacion item)
        {
            using (var db = DbConnection.Create())
            {
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        var fecha = DateTime.Now;

                        item.fechaAcreditacion = fecha;
                        db.Acreditacion.Add(item);

                        db.SaveChanges();

                        db.Saldo.Add(new Saldo
                        {
                            identificadorExterno = item.id,
                            tipo = (long)SaldoTipo.acreditacion,
                            puntoVentaId = item.puntoVentaId,
                            monto = item.valor,
                            fecha = fecha
                        });

                        db.SaveChanges();
                        transaccion.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        throw ex;
                    }
                }
            }

            return item.id;
        }
    }
}
