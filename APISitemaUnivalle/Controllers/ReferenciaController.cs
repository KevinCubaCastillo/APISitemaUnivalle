using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Publicacion;
using APISitemaUnivalle.Models.Request.Referencias;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciaController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public ReferenciaController(dbUnivalleContext context)
        {
            _context = context;
        }

        [HttpGet("getAllReferences")]
        public IActionResult getAllReferences()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Referencia;
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


        [HttpGet("GetActiveReferences")]
        public IActionResult GetActiveReferences()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Referencia.Where(p => p.Estado);
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron referencias activas";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos.ToList(); 
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpGet("GetInactiveReferences")]
        public IActionResult GetInactiveReferences()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Referencia.Where(p => !p.Estado);
                if (!datos.Any())
                {
                    oResponse.message = "No se encontraron referencias inactivas";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos.ToList();
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }


        [HttpGet("GetReferenceById/{id}")]
        public IActionResult GetReferenceById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var referencia = _context.Referencia.FirstOrDefault(p => p.Id == id);

                if (referencia == null)
                {
                    oResponse.message = "No se encontró la referencia con el ID proporcionado";
                    return BadRequest(oResponse);
                }

                oResponse.data = referencia;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }

            return Ok(oResponse);
        }
        [HttpGet("getReferenciasbyServicioId/{id}")]
        public IActionResult getReferenciasbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Referencia.Where(r => r.Estado == true && r.ServiciosId == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Nombre,
                    numero = i.Numerocel,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado
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
        [HttpPost("AddReferences")]
        public IActionResult addCliente(Referencias_add_Request oreferencias)
        {
            Response oResponse = new Response();
            try
            {
                Referencium nreferencia = new Referencium();
                nreferencia.Nombre = oreferencias.Nombre;
                nreferencia.Numerocel = oreferencias.Numerocel;
                nreferencia.ServiciosId = oreferencias.ServiciosId;
                nreferencia.IdModulo = oreferencias.id_modulo;
                nreferencia.Estado= oreferencias.Estado;

                _context.Referencia.Add(nreferencia);
                _context.SaveChanges();
                oResponse.message = "agregado exitosamente";
                oResponse.success = 1;
                oResponse.data = nreferencia;
                return Ok(oResponse);
            }

            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);

        }
        [HttpPut("UpdateReferences/{id}")]
        public IActionResult UpdateReferences(Referencias_edit_Request oReferencia, int id)
        {
            Response oResponse = new Response();
            try
            {
                var referencia = _context.Referencia.Find(id);
                if (referencia == null)
                {
                    oResponse.message = "La referencia no existe";
                    return Ok(oResponse);
                }
                referencia.Nombre = oReferencia.Nombre;
                referencia.Numerocel = oReferencia.Numerocel;
                referencia.ServiciosId = oReferencia.ServiciosId;

                _context.Referencia.Update(referencia);
                _context.SaveChanges();
                oResponse.message = "editado con exito";
                oResponse.success = 1;
                oResponse.data = referencia;
                return Ok(oResponse);
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }

        [HttpPut("DeleteReferences")]
        public IActionResult DeleteReferences(int id)
        {
            Response oResponse = new Response();
            try
            {
                var referencias = _context.Referencia.Find(id);
                if (referencias == null)
                {
                    oResponse.message = "La referencia no existe";
                    return Ok(oResponse);
                }
                if (referencias.Estado == false)
                {
                    oResponse.message = "La referencia ya esta desactivada";
                    return Ok(oResponse);
                }
                referencias.Estado = false;
                _context.Referencia.Update(referencias);
                _context.SaveChanges();

                oResponse.message = "La referencia ha sido desactivada";
                oResponse.success = 1;
                oResponse.data = referencias;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpPut("RestoreReferences")]
        public IActionResult RestoreReferences(int id)
        {
            Response oResponse = new Response();
            try
            {
                var referencias = _context.Referencia.Find(id);
                if (referencias == null)
                {
                    oResponse.message = "La referencia no existe";
                    return Ok(oResponse);
                }
                if (referencias.Estado == true)
                {
                    oResponse.message = "La referencia ya esta activada";
                    return Ok(oResponse);
                }
                referencias.Estado = true;
                _context.Referencia.Update(referencias);
                _context.SaveChanges();

                oResponse.message = "La referencia ha sido activada";
                oResponse.success = 1;
                oResponse.data = referencias;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }

    }
}
