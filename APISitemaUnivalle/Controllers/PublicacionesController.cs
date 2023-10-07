using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Publicacion;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public PublicacionesController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllPublications")]
        public IActionResult getAllPublications()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Publicacions;
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos validos";
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
        [HttpGet("GetActivePublicaciones")]
        public IActionResult GetActivePublicaciones()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Publicacions.Where(p => p.Estado);
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron publicaciones activas";
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
        [HttpGet("GetInactivePublicaciones")]
        public IActionResult GetInactivePublicaciones()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Publicacions.Where(p => !p.Estado);
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron publicaciones inactivas";
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
        [HttpGet("GetPublicacionByID/{id}")]
        public IActionResult GetPublicacionByID(int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.FirstOrDefault(p => p.Id == id);

                if (publicacion == null)
                {
                    oResponse.message = "No se encontró la publicación con el ID proporcionado";
                    return BadRequest(oResponse);
                }

                oResponse.data = publicacion;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPost("AddPublicaciones")]
        public IActionResult addCliente(Publicacion_add_Request oPublicacion)
        {
            Response oResponse = new Response();
            try
            {
                
                Publicacion npublicacion = new Publicacion();
                //npublicacion.Id = oPublicacion.Id;
                npublicacion.Archivo = oPublicacion.Archivo;
                npublicacion.Descripcion1 = oPublicacion.Descripcion1;
                npublicacion.ServiciosId = oPublicacion.ServiciosId;
                npublicacion.Titulo = oPublicacion.Titulo;
                npublicacion.Descripcion2 = oPublicacion.Descripcion2;
                npublicacion.Estado = oPublicacion.Estado;
                _context.Publicacions.Add(npublicacion);
                _context.SaveChanges();
                oResponse.message = "agregado con exito";
                oResponse.success = 1;
                oResponse.data = npublicacion;
                return Ok(oResponse);
            }

            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);

        }
        [HttpPut("UpdatePublicaciones")]
        public IActionResult UpdatePublicaciones(Publicacion_edit_Request oPublicacion, int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Find(id);
                if (publicacion == null)
                {
                    oResponse.message = "El cliente no existe";
                    return Ok(oResponse);
                }
                publicacion.Archivo = oPublicacion.Archivo;
                publicacion.Descripcion1 = oPublicacion.Descripcion1;
                publicacion.ServiciosId = oPublicacion.ServiciosId;
                publicacion.Titulo = oPublicacion.Titulo;
                publicacion.Descripcion2 = oPublicacion.Descripcion2;

                _context.Publicacions.Update(publicacion);
                _context.SaveChanges();
                oResponse.message = "editado con exito";
                oResponse.success = 1;
                oResponse.data = publicacion;
                return Ok(oResponse);
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpPut("DeletePublicaciones")]
        public IActionResult DeletePublicaciones(int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Find(id);
                if (publicacion == null)
                {
                    oResponse.message = "La publicación no existe";
                    return Ok(oResponse);
                }
                if (publicacion.Estado == false)
                {
                    oResponse.message = "La publicacion no existe";
                    return Ok(oResponse);
                }
                publicacion.Estado = false;
                _context.Publicacions.Update(publicacion);
                _context.SaveChanges();

                oResponse.message = "La publicación ha sido desactivada";
                oResponse.success = 1;
                oResponse.data = publicacion;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }



        [HttpPut("RestorePublicaciones")]
        public IActionResult ActivarPublicacion(int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Find(id);
                if (publicacion == null)
                {
                    oResponse.message = "La publicación no existe";
                    return Ok(oResponse);
                }
                if (publicacion.Estado == true)
                {
                    oResponse.message = "La publicacion ya existe";
                    return Ok(oResponse);
                }
                publicacion.Estado = true;
                _context.Publicacions.Update(publicacion);
                _context.SaveChanges();

                oResponse.message = "La publicación ha sido activada";
                oResponse.success = 1;
                oResponse.data = publicacion;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }


    }
}
