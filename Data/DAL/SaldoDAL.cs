using Common.Util;
using Common.ViewModels;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class SaldoDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static long Post(Saldo item)
        {
            using (var db = DbConnection.Create())
            {
                item.fecha = DateTime.Now;
                db.Saldo.Add(item);
                db.SaveChanges();
            }

            return item.id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdvId"></param>
        /// <returns></returns>
        public static SaldoVMR GetSaldoDePDV(long pdvId)
        {
            SaldoVMR result = new SaldoVMR();

            using (var db = DbConnection.Create())
            {
                var acreditacion = db.Saldo.Where(s => s.puntoVentaId == pdvId && s.tipo == (long)SaldoTipo.acreditacion).Select(s => s.monto).DefaultIfEmpty(0).Sum();
                var transaccionado = db.Saldo.Where(s => s.puntoVentaId == pdvId && s.tipo == (long)SaldoTipo.transaccionPago).Select(s => s.monto).DefaultIfEmpty(0).Sum();
                result.comision = db.Saldo.Where(s => s.puntoVentaId == pdvId && s.tipo == (long)SaldoTipo.comision).Select(s => s.monto).DefaultIfEmpty(0).Sum();

                result.saldo = acreditacion - transaccionado;

                result.total = result.saldo + result.comision;
            }

            return result;
        }
    }
}
