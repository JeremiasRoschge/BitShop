using System;
using System.Net;
using System.Web.Http;
using Irecaudo.WebApi.ActionFilters;
using server.Models;

namespace server.Controllers
{
    [CustomSimpleAuthorizationFilter]
    public class EmpleadoController : ApiController
    {
        [HttpGet]
        [Route("api/ObtenerEmpleado")]
        public IHttpActionResult obtenerEmpleado()
        {
            try
            {
                GestorEmpleado gestorEmpleado = new GestorEmpleado();
                var resultado = gestorEmpleado.getEmpleados();


                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/AgregarEmpleado")]
        public IHttpActionResult AgregarEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                GestorEmpleado gestorEmpleado = new GestorEmpleado();

                
                bool resultado = gestorEmpleado.addEmpleado(empleado);

                if (resultado)
                {
                    // Si se agrega el empleado con éxito, devuelve una respuesta HTTP 201 (Created)
                    return Created(Request.RequestUri, resultado);
                }
                else
                {
                    // Si no se puede agregar el empleado, devuelve una respuesta HTTP 400 (Bad Request)
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // En caso de error, devuelve una respuesta HTTP 500 (Internal Server Error) con información de error
                return InternalServerError(ex);
            }
        }
    }
}