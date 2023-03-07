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
    public class ItemBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ItemVMR> ObtenerProductosActivos()
        {
            return ItemDAL.ObtenerProductosActivos();
        }

        /// <summary>
        /// Método para obtener todos los items
        /// </summary>
        /// <param name="cantidad">Cantidad de elementos a obtener</param>
        /// <param name="pagina">Número de página de resultados</param>
        /// <param name="textoBusqueda">Texto para filtrar la búsqueda</param>
        public static ListadoPaginadoVMR<ItemVMR> Gets(int cantidad, int pagina, string textoBusqueda)
        {
            return ItemDAL.Gets(cantidad, pagina, textoBusqueda);
        }

        /// <summary>
        /// Método para obtener item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea obtener</param>
        public static ItemVMR Get(long id)
        {
            return ItemDAL.Get(id);
        }

        /// <summary>
        /// Método para crear un nuevo item y retornar su identificador
        /// </summary>
        /// <param name="item">Datos del nuevo item</param>
        public static long Post(Item item)
        {
            return ItemDAL.Post(item);
        }

        /// <summary>
        /// Método para editar un item
        /// </summary>
        /// <param name="item">Datos del item a editar</param>
        public static void Put(ItemVMR item)
        {
            ItemDAL.Put(item);
        }

        /// <summary>
        /// Método para borrar varios items por sus identificadores
        /// </summary>
        /// <param name="ids">Identificadores de los items que se desean borrar</param>
        public static void Delete(List<long> ids)
        {
            ItemDAL.Delete(ids);
        }

        /// <summary>
        /// Método para comprobar la existencia de un item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea comprobar su existencia</param>
        public static bool ItemExists(long id)
        {
            return ItemDAL.ItemExists(id);
        }
    }
}
