using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Modulos;
using APISitemaUnivalle.Models.Request.Ubicacion;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionesController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public UbicacionesController(dbUnivalleContext context)
        {
            _context = context;
        }

        [HttpGet("getAllUbicaciones")]
        public IActionResult getAllUbicaciones()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Ubicaciones;
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }


        [HttpGet("getActiveUbicaciones")]
        public IActionResult getActiveClientes()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Ubicaciones.Where(i => i.Estado == true).Select(i => new
                {
                    id = i.Id,
                    descripcion = i.Descripcion,
                    imagen = i.Imagen,
                    video = i.Video,
                    servicios_id = i.ServiciosId,
                    estado = i.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpGet("getInactiveUbicaciones")]
        public IActionResult GetInactiveUbicaciones()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Ubicaciones.Where(i => i.Estado == false).Select(i => new
                {
                    id = i.Id,
                    descripcion = i.Descripcion,
                    imagen = i.Imagen,
                    video = i.Video,
                    servicios_id = i.ServiciosId,
                    estado = i.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpGet("getUbicacionByID/{id}")]
        public IActionResult getUbicacionByID(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Ubicaciones.Where(i => i.Id == id).Select(i => new
                {
                    id = i.Id,
                    descripcion = i.Descripcion,
                    imagen = i.Imagen,
                    video = i.Video,
                    servicios_id = i.ServiciosId,
                    estado = i.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontro";
                    return Ok(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }
        [HttpGet("getUbicacionesbyServicioId/{id}")]
        public IActionResult getUbicacionesbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Ubicaciones.Where(r => r.Estado == true && r.ServiciosId == id).Select(i => new
                {
                    Identificador = i.Id,
                    descripcion = i.Descripcion,
                    i.Imagen,
                    i.Video,
                    servicio = i.Servicios.Nombre,
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
                oResponse.message = "Solicitud realizada con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPost("addUbicaciones")]
        public IActionResult addUbicaciones(ubicacion_add_request UbicacionModel)
        {
            Response oResponse = new Response();
            Ubicacione ubicacion = new Ubicacione();
            try
            {

                
                ubicacion.Descripcion = UbicacionModel.Descripcion;
                ubicacion.Imagen = UbicacionModel.Imagen;
                ubicacion.Video = UbicacionModel.Video;
                ubicacion.ServiciosId = UbicacionModel.ServiciosId;
                ubicacion.Estado = UbicacionModel.Estado;
          
                _context.Ubicaciones.Add(ubicacion);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Ubicacion registrada con exito";
                oResponse.data = ubicacion;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }


        [HttpPut("updateUbicaciones")]
        public IActionResult updateUbicaciones(ubicacion_add_request UbicacionModel, int id)
        {
            Response oResponse = new Response();
            
            try
            {
                var ubicacion = _context.Ubicaciones.Find(id);

                if (ubicacion == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
               
                ubicacion.Descripcion = UbicacionModel.Descripcion;
                ubicacion.Imagen = UbicacionModel.Imagen;
                ubicacion.Video = UbicacionModel.Video;
                ubicacion.ServiciosId = UbicacionModel.ServiciosId;
                ubicacion.Estado = true;

                _context.Ubicaciones.Update(ubicacion);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Ubicacion modificada con exito";
                oResponse.data = ubicacion;

            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }



        [HttpPut("deleteUbicacion/{id}")]
        public IActionResult deleteUbicacion(int id)
        {
            Response oResponse = new Response();
            try
            {
                var ubicacion = _context.Ubicaciones.Find(id);
                if (ubicacion == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                if (ubicacion.Estado == false)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                ubicacion.Estado = false;
                _context.Ubicaciones.Update(ubicacion);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Ubicacion eliminada con exito";
                oResponse.data = ubicacion;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreUbicacion/{id}")]
        public IActionResult restoreUbicacion(int id)
        {
            Response oResponse = new Response();
            try
            {
                var ubicacion = _context.Ubicaciones.Find(id);
                if (ubicacion == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                if (ubicacion.Estado == true)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                ubicacion.Estado = true;
                _context.Ubicaciones.Update(ubicacion);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Ubicacion restaurada con exito";
                oResponse.data = ubicacion;
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
