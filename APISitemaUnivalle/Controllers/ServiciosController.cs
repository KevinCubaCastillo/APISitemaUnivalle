using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Servicios;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public ServiciosController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllServicios")]
        public IActionResult getAllServicios()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl
                });
                if(datos.Count() == 0)
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
        [HttpGet("getActiveServicios")]
        public IActionResult getActiveServicios()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == true).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl
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
        [HttpGet("getDeletedServicios")]
        public IActionResult getDeletedServicios()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == false).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl
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
        [HttpGet("getServicioById/{id}")]
        public IActionResult getServicioById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Include(e => e.Ubicaciones).Include(e => e.Referencia).Where(e=> e.Id == id);
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
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }
        [HttpGet("getServicioByModule/{name}")]
        public IActionResult getServicioByModule(string name)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Include(e => e.Ubicaciones).Include(e => e.Referencia).Where(e => e.Modulo.Nombremodulo.Equals(name));
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
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }
        [HttpPost ("addServicio")]
        public IActionResult addServicio(servicio_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verify = _context.Servicios.FirstOrDefault(i => (i.Nombre).ToUpper() == (oModel.Nombre).ToUpper() && i.ModuloId == oModel.ModuloId);
                if(verify != null)
                {
                    oResponse.message = "El servicio ya existe";
                    return BadRequest(oResponse);
                }
                Servicio servicio = new Servicio();
                servicio.Nombre = oModel.Nombre;
                servicio.ModuloId = oModel.ModuloId;
                servicio.ImagenUrl = oModel.ImagenUrl;
                servicio.Estado = true;
                _context.Servicios.Add(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio registrado con exito";
                oResponse.data = servicio;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateServicio/{id}")]
        public IActionResult updateServicio(servicio_update_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var servicio = _context.Servicios.Find(id);
                if (servicio == null)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                servicio.Nombre = oModel.Nombre;
                servicio.ImagenUrl = oModel.ImagenUrl;
                servicio.Estado = true;
                _context.Servicios.Update(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio actualizado con exito";
                oResponse.data = servicio;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteServicio/{id}")]
        public IActionResult deleteServicio(int id)
        {
            Response oResponse = new Response();
            try
            {
                var servicio = _context.Servicios.Find(id);
                if (servicio == null)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                if(servicio.Estado == false)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                servicio.Estado = false;
                _context.Servicios.Update(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio eliminado con exito";
                oResponse.data = servicio;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreServicio/{id}")]
        public IActionResult restoreServicio(int id)
        {
            Response oResponse = new Response();
            try
            {
                var servicio = _context.Servicios.Find(id);
                if (servicio == null)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                if (servicio.Estado == true)
                {
                    oResponse.message = "El servicio no esta eliminado";
                    return BadRequest(oResponse);
                }
                servicio.Estado = true;
                _context.Servicios.Update(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio restaurado con exito";
                oResponse.data = servicio;
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
