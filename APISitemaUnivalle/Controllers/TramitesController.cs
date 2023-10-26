using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Servicios;
using APISitemaUnivalle.Models.Request.Tramites;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TramitesController : Controller
    {
        private readonly dbUnivalleContext _context;
        public TramitesController(dbUnivalleContext context)
        {
            _context = context;
        }

        [HttpGet("getAllTramites")]
        public IActionResult getAllTramites()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Tramites.Select(i => new
                {
                    identificador = i.Id,
                    tiempoTramite = i.Tiempotramite,
                    servicio = i.Servicios.Nombre
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.message = "Solicitud realizada con exito";
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }


        [HttpGet("getActiveTramites")]
        public IActionResult getActiveTramites()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Tramites.Where(i => i.Estado == true).Select(i => new
                {
                    identificador = i.Id,
                    tiempoTramite = i.Tiempotramite,
                    servicio = i.Servicios.Nombre
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.message = "Solicitud realizada con exito";
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getDeletedTramites")]
        public IActionResult getDeletedTramites()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Tramites.Where(i => i.Estado == false).Select(i => new
                {
                    identificador = i.Id,
                    tiempoTramite = i.Tiempotramite,
                    servicio = i.Servicios.Nombre
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.message = "Solicitud realizada con exito";
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpGet("getTramiteById/{id}")]
        public IActionResult getModuleById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Tramites.Find(id);
                if (datos == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.message = "Solicitud realizada con exito";
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPost("addTramite")]
        public IActionResult addTramite(tramite_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verify = _context.Tramites.FirstOrDefault(i => (i.Tiempotramite).ToUpper() == (oModel.Tiempotramite).ToUpper() && i.Servicios.Id == oModel.ServiciosId);
                if (verify != null)
                {
                    oResponse.message = "El tramite ya existe";
                    return BadRequest(oResponse);
                }
                Tramite tramite = new Tramite();
                tramite.Tiempotramite = oModel.Tiempotramite;
                tramite.ServiciosId = oModel.ServiciosId;
                tramite.Estado = true;
                _context.Tramites.Add(tramite);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Tramite registrado con exito";
                oResponse.data = tramite;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPut("updateTramite/{id}")]
        public IActionResult updateTramite(tramite_update_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var tramite = _context.Tramites.Find(id);
                if (tramite == null)
                {
                    oResponse.message = "El tramite no existe";
                    return BadRequest(oResponse);
                }
                tramite.Tiempotramite = oModel.Tiempotramite;
                tramite.Estado = true;

                _context.Tramites.Update(tramite);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Tramite actualizado con exito";
                oResponse.data = tramite;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }

        [HttpPut("deleteServicio/{id}")]
        public IActionResult deleteServicio(int id)
        {
            Response oResponse = new Response();
            try
            {
                var tramite = _context.Tramites.Find(id);
                if (tramite == null)
                {
                    oResponse.message = "El tramite no existe";
                    return BadRequest(oResponse);
                }
                if (tramite.Estado == false)
                {
                    oResponse.message = "El tramite no existe";
                    return BadRequest(oResponse);
                }
                tramite.Estado = false;
                _context.Tramites.Update(tramite);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Tramite eliminado con exito";
                oResponse.data = tramite;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }


        [HttpPut("restoreTramite/{id}")]
        public IActionResult restoreServicio(int id)
        {
            Response oResponse = new Response();
            try
            {
                var tramite = _context.Tramites.Find(id);
                if (tramite == null)
                {
                    oResponse.message = "El tramite no existe";
                    return BadRequest(oResponse);
                }
                if (tramite.Estado == true)
                {
                    oResponse.message = "El tramite no esta eliminado";
                    return BadRequest(oResponse);
                }
                tramite.Estado = true;
                _context.Tramites.Update(tramite);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Tramite restaurado con exito";
                oResponse.data = tramite;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
    }
}
