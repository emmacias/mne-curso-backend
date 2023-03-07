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
    public class AcreditacionBLL
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
            return AcreditacionDAL.Gets(cantidad, pagina, textoBusqueda, puntoVentaId);
        }

        /// <summary>
        /// Método para obtener item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea obtener</param>
        public static AcreditacionVMR Get(long id)
        {
            return AcreditacionDAL.Get(id);
        }

        /// <summary>
        /// Método para crear un nuevo item y retornar su identificador
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static long Post(Acreditacion item)
        {
            return AcreditacionDAL.Post(item);
        }

        /// <summary>
        /// Método para editar un item
        /// </summary>
        /// <param name="item">Datos del item a editar</param>
        public static void Put(AcreditacionVMR item)
        {
            AcreditacionDAL.Put(item);
        }

        /// <summary>
        /// Método para borrar varios items por sus identificadores
        /// </summary>
        /// <param name="ids">Identificadores de los items que se desean borrar</param>
        public static void Delete(List<long> ids)
        {
            AcreditacionDAL.Delete(ids);
        }

        /// <summary>
        /// Método para comprobar la existencia de un item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea comprobar su existencia</param>
        public static bool ItemExists(long id)
        {
            return AcreditacionDAL.ItemExists(id);
        }

        /// <summary>
        /// Método para obtener los datos necesarios para el formulario
        /// </summary>
        /// <param name="id">Identificador del item (0 cuando el formulario es de creación)</param>
        /// <returns></returns>
        public static AcreditacionFormDataVMR GetFormData(long id)
        {
            return AcreditacionDAL.GetFormData(id);
        }
    }
}
