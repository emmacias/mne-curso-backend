using Common.Util;
using Common.ViewModels;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class AcreditacionDAL
    {
        /// <summary>
        /// Método para obtener todos los items
        /// </summary>
        /// <param name="cantidad">Cantidad de elementos a obtener</param>
        /// <param name="pagina">Número de página de resultados</param>
        /// <param name="textoBusqueda">Texto para filtrar la búsqueda</param>
        /// <param name="puntoVentaId"></param>
        public static ListadoPaginadoVMR<AcreditacionVMR> Gets(int cantidad, int pagina, string textoBusqueda, long puntoVentaId)
        {
            ListadoPaginadoVMR<AcreditacionVMR> result = new ListadoPaginadoVMR<AcreditacionVMR>();

            using (var db = DbConnection.Create())
            {
                // Consulta base
                var query = db.Acreditacion.Where(x => x.puntoVentaId == puntoVentaId).Select(x => new AcreditacionVMR()
                {
                    id = x.id,
                    fechaAcreditacion = x.fechaAcreditacion,
                    valor = x.valor,
                    formaPago = new FormaPagoVMR
                    {
                        nombre = x.FormaPago.nombre
                    }
                });

                // Filtrado por texto de búsqueda
                if (textoBusqueda != null && textoBusqueda.Length > 0)
                {
                    query = query.Where(x =>
                        x.formaPago.nombre.Contains(textoBusqueda)
                    );
                }

                // Conteo total de elementos
                result.cantidadTotal = query.Count();

                // Paginado
                result.elementos = query
                    .OrderBy(x => x.id)
                    .Skip(pagina * cantidad)
                    .Take(cantidad)
                    .ToList();
            }

            return result;
        }

        /// <summary>
        /// Método para obtener item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea obtener</param>
        public static AcreditacionVMR Get(long id)
        {
            AcreditacionVMR item = null;

            using (var db = DbConnection.Create())
            {
                item = db.Acreditacion.Where(x => x.id == id).Select(x => new AcreditacionVMR()
                {
                    id = x.id,
                    fechaAcreditacion = x.fechaAcreditacion,
                    valor = x.valor,
                    puntoVentaId = x.puntoVentaId,
                    formaPagoId = x.formaPagoId
                }).FirstOrDefault();
            }

            return item;
        }

        /// <summary>
        /// Método para crear un nuevo item y retornar su identificador
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

        /// <summary>
        /// Método para editar un item
        /// </summary>
        /// <param name="item">Datos del item a editar</param>
        public static void Put(AcreditacionVMR item)
        {
            using (var db = DbConnection.Create())
            {
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        var itemUpdate = db.Acreditacion.Find(item.id);

                        itemUpdate.valor = item.valor;
                        itemUpdate.formaPagoId = item.formaPagoId;

                        db.Entry(itemUpdate).State = EntityState.Modified;

                        var saldoUpdate = db.Saldo.Where(s => s.identificadorExterno == item.id && s.tipo == (long)SaldoTipo.acreditacion).FirstOrDefault();
                        saldoUpdate.monto = item.valor;

                        db.Entry(saldoUpdate).State = EntityState.Modified;

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
        }

        /// <summary>
        /// Método para borrar varios items por sus identificadores
        /// </summary>
        /// <param name="ids">Identificadores de los items que se desean borrar</param>
        public static void Delete(List<long> ids)
        {
            using (var db = DbConnection.Create())
            {
                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        var items = db.Acreditacion.Where(x => ids.Contains(x.id));

                        foreach (var item in items)
                        {
                            var saldoDelete = db.Saldo.Where(s => s.identificadorExterno == item.id && s.tipo == (long)SaldoTipo.acreditacion).FirstOrDefault();

                            db.Acreditacion.Remove(item);
                            db.Saldo.Remove(saldoDelete);
                        }

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
        }

        /// <summary>
        /// Método para comprobar la existencia de un item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea comprobar su existencia</param>
        public static bool ItemExists(long id)
        {
            var result = false;

            using (var db = DbConnection.Create())
            {
                result = db.Acreditacion.Any(x => x.id == id);
            }

            return result;
        }

        /// <summary>
        /// Método para obtener los datos necesarios para el formulario
        /// </summary>
        /// <param name="id">Identificador del item (0 cuando el formulario es de creación)</param>
        /// <returns></returns>
        public static AcreditacionFormDataVMR GetFormData(long id)
        {
            AcreditacionFormDataVMR datos = new AcreditacionFormDataVMR();

            using (var db = DbConnection.Create())
            {
                long formaPagoId = 0;

                if (id != 0)
                {
                    formaPagoId = db.Acreditacion.Where(x => x.id == id).Select(x => x.formaPagoId).FirstOrDefault();
                }

                datos.formaPagoList = db.FormaPago.Where(x => x.habilitado || x.id == formaPagoId).Select(x => new FormaPagoVMR
                {
                    id = x.id,
                    nombre = x.nombre
                }).ToList();
            }

            return datos;
        }
    }
}
