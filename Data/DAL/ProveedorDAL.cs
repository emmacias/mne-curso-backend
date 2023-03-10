using Common.Util;
using Common.ViewModels;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class ProveedorDAL
    {
        /// <summary>
        /// Método para obtener todos los items
        /// </summary>
        /// <param name="cantidad">Cantidad de elementos a obtener</param>
        /// <param name="pagina">Número de página de resultados</param>
        /// <param name="textoBusqueda">Texto para filtrar la búsqueda</param>
        public static ListadoPaginadoVMR<ProveedorVMR> Gets(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<ProveedorVMR> result = new ListadoPaginadoVMR<ProveedorVMR>();

            using (var db = DbConnection.Create())
            {
                // Consulta base
                var query = db.Proveedor.Select(x => new ProveedorVMR()
                {
                    id = x.id,
                    nombre = x.nombre,
                    bloqueado = x.bloqueado,
                    urlServicios = x.urlServicios
                });

                // Filtrado por texto de búsqueda
                if (textoBusqueda != null && textoBusqueda.Length > 0)
                {
                    query = query.Where(x =>
                        x.nombre.Contains(textoBusqueda)
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
        public static ProveedorVMR Get(long id)
        {
            ProveedorVMR item = null;

            using (var db = DbConnection.Create())
            {
                item = db.Proveedor.Where(x => x.id == id).Select(x => new ProveedorVMR()
                {
                    id = x.id,
                    nombre = x.nombre,
                    bloqueado = x.bloqueado,
                    urlServicios = x.urlServicios,
                    TelefonoContacto = db.TelefonoContacto.Where(t => t.proveedorId == x.id).Select(t => new TelefonoContactoVMR
                    {
                        id = t.id,
                        numero = t.numero,
                        telefonoOperadoraId = t.telefonoOperadoraId
                    }).ToList()
                }).FirstOrDefault();
            }

            return item;
        }

        /// <summary>
        /// Método para crear un nuevo item y retornar su identificador
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static long Post(Proveedor item)
        {
            using (var db = DbConnection.Create())
            {
                db.Proveedor.Add(item);

                db.SaveChanges();
            }

            return item.id;
        }

        /// <summary>
        /// Método para editar un item
        /// </summary>
        /// <param name="item">Datos del item a editar</param>
        public static void Put(ProveedorVMR item)
        {
            using (var db = DbConnection.Create())
            {
                var itemUpdate = db.Proveedor.Find(item.id);

                itemUpdate.nombre = item.nombre;
                itemUpdate.bloqueado = item.bloqueado;
                itemUpdate.urlServicios = item.urlServicios;

                // Se obtienen los teléfonos existentes del proveedor
                var telefonosExistentes = db.TelefonoContacto.Where(t => t.proveedorId == item.id).ToList();

                // Se iteran los contactos de teléfono para actualizar (los existentes) o adicionar (los nuevos)
                foreach (var telefonoActualizar in item.TelefonoContacto)
                {
                    var telefonoExistente = telefonosExistentes.FirstOrDefault(p => p.id == telefonoActualizar.id);

                    // Se editan los teléfonos existentes
                    if (telefonoExistente != null)
                    {
                        telefonoExistente.numero = telefonoActualizar.numero;
                        telefonoExistente.telefonoOperadoraId = telefonoActualizar.telefonoOperadoraId;

                        db.Entry(telefonoExistente).State = EntityState.Modified;
                    }
                    // se adicionan los teléfonos nuevos
                    else
                    {
                        db.TelefonoContacto.Add(new TelefonoContacto { 
                            numero = telefonoActualizar.numero,
                            proveedorId = item.id,
                            telefonoOperadoraId = telefonoActualizar.telefonoOperadoraId
                        });
                    }
                }

                // Se iteran los contactos de teléfono existentes para eliminar (los que no se enviaron desde el frontend)
                foreach (var telefonoExistente in telefonosExistentes)
                {
                    if (!item.TelefonoContacto.Any(p => p.id == telefonoExistente.id))
                    {
                        db.TelefonoContacto.Remove(telefonoExistente);
                    }
                }


                db.Entry(itemUpdate).State = EntityState.Modified;

                db.SaveChanges();
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
                using (var dbcxtransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var telefonos = db.TelefonoContacto.Where(x => ids.Contains(x.proveedorId));

                        db.TelefonoContacto.RemoveRange(telefonos);

                        var items = db.Proveedor.Where(x => ids.Contains(x.id));

                        db.Proveedor.RemoveRange(items);

                        db.SaveChanges();

                        dbcxtransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbcxtransaction.Rollback();
                        throw e;
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
                result = db.Proveedor.Any(x => x.id == id);
            }

            return result;
        }

        /// <summary>
        /// Método para obtener los datos necesarios para el formulario
        /// </summary>
        /// <param name="id">Identificador del item (0 cuando el formulario es de creación)</param>
        /// <returns></returns>
        public static ProveedorFormDataVMR GetFormData(long id)
        {
            ProveedorFormDataVMR datos = new ProveedorFormDataVMR();

            using (var db = DbConnection.Create())
            {
                datos.telefonoOperadoraList = db.TelefonoOperadora.Select(x => new TelefonoOperadoraVMR
                {
                    id = x.id,
                    nombre = x.nombre
                }).ToList();
            }

            return datos;
        }
    }
}
