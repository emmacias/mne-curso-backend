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
    public class ItemDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ItemVMR> ObtenerProductosActivos()
        {
            List<ItemVMR> result = new List<ItemVMR>();

            using (var db = DbConnection.Create())
            {
                //result = db.Item.Where(i => 
                //    !i.borrado 
                //    && i.habilitado
                //    && i.Proveedor.bloqueado != true
                //).OrderBy(i => i.orden).Select(i => new ItemVMR { 
                //    id = i.id,
                //    nombre = i.nombre,
                //    imagen = i.imagen
                //}).ToList();

                result = db.Database.SqlQuery<ItemVMR>(@"
                    exec ObtenerProductosActivos 
                ").ToList();
            }

            return result;
        }

        /// <summary>
        /// Método para obtener todos los items
        /// </summary>
        /// <param name="cantidad">Cantidad de elementos a obtener</param>
        /// <param name="pagina">Número de página de resultados</param>
        /// <param name="textoBusqueda">Texto para filtrar la búsqueda</param>
        public static ListadoPaginadoVMR<ItemVMR> Gets(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<ItemVMR> result = new ListadoPaginadoVMR<ItemVMR>();

            using (var db = DbConnection.Create())
            {
                // Consulta base
                var query = db.Item.Where(x => !x.borrado).Select(x => new ItemVMR()
                {
                    id = x.id,
                    nombre = x.nombre,
                    imagen = x.imagen
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
        public static ItemVMR Get(long id)
        {
            ItemVMR item = null;

            using (var db = DbConnection.Create())
            {
                item = db.Item.Where(x => x.id == id && !x.borrado).Select(x => new ItemVMR()
                {
                    id = x.id,
                    nombre = x.nombre,
                    imagen = x.imagen
                }).FirstOrDefault();
            }

            return item;
        }

        /// <summary>
        /// Método para crear un nuevo item y retornar su identificador
        /// </summary>
        /// <param name="item">Datos del nuevo item</param>
        public static long Post(Item item)
        {
            using (var db = DbConnection.Create())
            {
                item.borrado = false;
                db.Item.Add(item);
                db.SaveChanges();
            }

            return item.id;
        }

        /// <summary>
        /// Método para editar un item
        /// </summary>
        /// <param name="item">Datos del item a editar</param>
        public static void Put(ItemVMR item)
        {
            using (var db = DbConnection.Create())
            {
                Item itemUpdate = db.Item.Find(item.id);

                itemUpdate.nombre = item.nombre;
                itemUpdate.imagen = item.imagen;

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
                var items = db.Item.Where(x => ids.Contains(x.id));

                foreach (var item in items)
                {
                    item.borrado = true;
                    db.Entry(item).State = EntityState.Modified;
                }

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
                result = db.Item.Any(x => x.id == id && !x.borrado);
            }

            return result;
        }
    }
}
