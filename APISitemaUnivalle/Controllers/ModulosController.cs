using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Modulos;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public ModulosController (dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllModulos")]
        public IActionResult getAllModulos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Modulos.Select(i => new
                {
                    Identificador = i.Id,
                    Nombre = i.Nombremodulo,
                    i.Estado
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
        [HttpGet("getActiveModulos")]
        public IActionResult getActiveModulos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Modulos.Where(i => i.Estado == true).Select(i => new
                {
                    Identificador = i.Id,
                    Nombre = i.Nombremodulo,
                    i.Estado
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
        [HttpGet("getDeletedModulos")]
        public IActionResult getDeletedModulos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Modulos.Where(i => i.Estado == false).Select(i => new
                {
                    Identificador = i.Id,
                    Nombre = i.Nombremodulo,
                    i.Estado
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
        [HttpGet("getModuloById/{id}")]
        public IActionResult getModuleById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Modulos.Find(id);
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
        [HttpPost("addModulo")]
        public IActionResult addModulo(modulos_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var ver = _context.Modulos.FirstOrDefault(i => (i.Nombremodulo).ToUpper() == (oModel.Nombremodulo).ToUpper());
                if (ver != null)
                {
                    oResponse.message = "El modulo ya existe";
                    return BadRequest(oResponse);
                }
                Modulo modulo = new Modulo();
                modulo.Nombremodulo = oModel.Nombremodulo;
                modulo.CiUsuario = "0000";
                modulo.Estado = true;
                _context.Modulos.Add(modulo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Modulo Registrado con exito";
                oResponse.data = modulo;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.InnerException.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateModulo/{id}")]
        public IActionResult updateModulo(modulos_update_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var modulo = _context.Modulos.Find(id);
                if (modulo == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                modulo.Nombremodulo = oModel.Nombremodulo;
                _context.Modulos.Update(modulo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Modulo actualizado con exito";
                oResponse.data = modulo;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteModulo/{id}")]
        public IActionResult deleteModulo(int id)
        {
            Response oResponse = new Response();
            try
            {
                var modulo = _context.Modulos.Find(id);
                if (modulo == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                if (modulo.Estado == false)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                modulo.Estado = false;
                _context.Modulos.Update(modulo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Modulo eliminado con exito";
                oResponse.data = modulo;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreModulo/{id}")]
        public IActionResult restoreModulo(int id)
        {
            Response oResponse = new Response();
            try
            {
                var modulo = _context.Modulos.Find(id);
                if (modulo == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                if (modulo.Estado == true)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                modulo.Estado = true;
                _context.Modulos.Update(modulo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Modulo restaurado con exito";
                oResponse.data = modulo;
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
