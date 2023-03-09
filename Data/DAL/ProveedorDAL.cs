﻿using Common.Util;
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
                    urlServicios = x.urlServicios
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
                var items = db.Proveedor.Where(x => ids.Contains(x.id));

                db.Proveedor.RemoveRange(items);

                db.SaveChanges();
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
    }
}
