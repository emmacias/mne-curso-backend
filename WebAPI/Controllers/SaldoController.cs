using Common.ViewModels;
using Logic.BLL;
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
    public class SaldoController : ApiController
    {
        // GET: api/saldo
        /// <summary>
        /// Servicio para obtener el saldo de un PDV
        /// </summary>
        /// <remarks>Servicio para obtener el saldo de un PDV</remarks>
        /// <response code="200">Ejecución exitosa</response>
        /// <response code="400">Error en los datos enviados</response>
        /// <response code="401">Token no válido o caducado</response>
        /// <response code="404">Recurso no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [ResponseType(typeof(RespuestaVMR<SaldoVMR>))]
        public IHttpActionResult GetSaldo()
        {
            var respuesta = new RespuestaVMR<SaldoVMR>();

            try
            {
                respuesta.datos = SaldoBLL.GetSaldoDePDV(1);
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
    }
}
