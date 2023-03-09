﻿using Common.ViewModels;
using Logic.BLL;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProveedorController : ApiController
    {
        // GET: api/proveedor
        /// <summary>
        /// Servicio para obtener todos los items
        /// </summary>
        /// <param name="cantidad">Cantidad de elementos a obtener</param>
        /// <param name="pagina">Número de página de resultados</param>
        /// <param name="textoBusqueda">Texto para filtrar la búsqueda</param>
        /// <remarks>Servicio para obtener todos los items</remarks>
        /// <response code="200">Ejecución exitosa</response>
        /// <response code="400">Error en los datos enviados</response>
        /// <response code="401">Token no válido o caducado</response>
        /// <response code="404">Recurso no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [ResponseType(typeof(RespuestaVMR<ListadoPaginadoVMR<ProveedorVMR>>))]
        public IHttpActionResult Gets(int cantidad = 20, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<ProveedorVMR>>();

            try
            {
                respuesta.datos = ProveedorBLL.Gets(cantidad, pagina, textoBusqueda);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        // GET: api/proveedor/{id}
        /// <summary>
        /// Servicio para obtener un item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea obtener</param>
        /// <remarks>Servicio para obtener un item por su identificador</remarks>
        /// <response code="200">Ejecución exitosa</response>
        /// <response code="400">Error en los datos enviados</response>
        /// <response code="401">Token no válido o caducado</response>
        /// <response code="404">Recurso no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [ResponseType(typeof(RespuestaVMR<ProveedorVMR>))]
        public IHttpActionResult Get(long id)
        {
            var respuesta = new RespuestaVMR<ProveedorVMR>();

            try
            {
                respuesta.datos = ProveedorBLL.Get(id);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            if (respuesta.datos == null && respuesta.mensajes.Count() == 0)
            {
                respuesta.codigo = HttpStatusCode.NotFound;
                respuesta.mensajes.Add("Elemento no encontrado.");
            }

            return Content(respuesta.codigo, respuesta);
        }

        // POST: api/proveedor
        /// <summary>
        /// Servicio para crear un nuevo item y retornar su identificador
        /// </summary>
        /// <param name="item">Datos del nuevo item</param>
        /// <remarks>Servicio para crear un nuevo item</remarks>
        /// <response code="200">Ejecución exitosa</response>
        /// <response code="400">Error en los datos enviados</response>
        /// <response code="401">Token no válido o caducado</response>
        /// <response code="404">Recurso no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [ResponseType(typeof(RespuestaVMR<long?>))]
        public IHttpActionResult Post(Proveedor item)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = ProveedorBLL.Post(item);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        // PUT: api/proveedor/{id}
        /// <summary>
        /// Servicio para editar un item por su identificador
        /// </summary>
        /// <param name="id">Identificador del item que se desea editar</param>
        /// <param name="item">Nuevos datos del item</param>
        /// <remarks>Servicio para editar un item por su identificador</remarks>
        /// <response code="200">Ejecución exitosa</response>
        /// <response code="400">Error en los datos enviados</response>
        /// <response code="401">Token no válido o caducado</response>
        /// <response code="404">Recurso no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [ResponseType(typeof(RespuestaVMR<bool>))]
        public IHttpActionResult Put(long id, ProveedorVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                ProveedorBLL.Put(item);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                try
                {
                    if (!ProveedorBLL.ItemExists(id))
                    {
                        respuesta.codigo = HttpStatusCode.NotFound;
                        respuesta.datos = false;
                        respuesta.mensajes.Add("Elemento no encontrado.");
                        return Content(respuesta.codigo, respuesta);
                    }
                }
                catch (Exception) { }

                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        // DELETE: api/proveedor
        /// <summary>
        /// Servicio para borrar varios items por su identificador
        /// </summary>
        /// <param name="ids">Ids de los items que se desean borrar</param>
        /// <remarks>Servicio para borrar varios items por su identificador</remarks>
        /// <response code="200">Ejecución exitosa</response>
        /// <response code="400">Error en los datos enviados</response>
        /// <response code="401">Token no válido o caducado</response>
        /// <response code="404">Recurso no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [ResponseType(typeof(RespuestaVMR<bool>))]
        public IHttpActionResult Delete(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                ProveedorBLL.Delete(ids);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        /// <summary>
        /// Liberar los recursos de la aplicación
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
