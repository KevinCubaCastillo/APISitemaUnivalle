using APISitemaUnivalle.Models.Response;
using APISitemaUnivalle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISitemaUnivalle.Models.Request.Requisitos;
using APISitemaUnivalle.Models.Request.PasosRequisitos;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasosRequisitosController : Controller
    {
        private readonly dbUnivalleContext _context;
        public PasosRequisitosController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllPasosRequisitos")]
        public IActionResult getAllPasosRequisitos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.PasosRequisitos.Select(i => new
                {

                    Id = i.Id,
                    Nombre = i.Nombre,
                    RequisitoId = i.RequisitosId,
                    i.Estado

                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
                oResponse.message = "Solicitud realizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }


        [HttpGet("getPasosRequisitosByRequisitoId/{id}")]
        public IActionResult getPasosByRequisitoId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.PasosRequisitos.Where(i => i.Estado == true && i.Requisitos.Id == id).Select(i => new
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
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
        [HttpGet("getActivePasosRequisitos")]
        public IActionResult getActivePasosRequisitos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.PasosRequisitos.Where(i => i.Estado == true).Select(i => new
                {
                  
                       Identificador = i.Id,
                       i.Nombre,
                       i.Requisitos.Descripcion,   
                       i.Estado
                
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
                oResponse.message = "Solicitud realizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getPasosRequisitosByServiceId")]
        public IActionResult getPasosRequisitosByServiceId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.PasosRequisitos.Where(i => i.Estado == true && i.Requisitos.Servicios.Id == id).Select(i => new
                {
                    Identificador = i.Id,
                    Nombre = i.Nombre,
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
                oResponse.message = "Solicitud realizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getDeletedPasosRequisitos")]
        public IActionResult getDeletedPasosRequisitos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.PasosRequisitos.Where(i => i.Estado == false).Select(i => new
                {
                    Identificador = i.Id,
                    i.Nombre,
                    i.Requisitos.Descripcion,
                    i.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
                oResponse.message = "Solicitud realizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpGet("getPasosRequisitosById/{id}")]
        public IActionResult getPasosRequisitosById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.PasosRequisitos.Where(i => i.Estado == true && i.Id == id).Select(i => new
                {
                 
                    Identificador = i.Id,
                    i.Nombre,
                    Requisito = i.Requisitos.Descripcion,
                    i.Estado
                 
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
                oResponse.message = "Solicitud realizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }
        [HttpPost("addPasoRequisito")]
        public IActionResult addPasoRequisito(pasos_add_request oModel)
        {
            Response oresponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        PasosRequisito pasoRequisito = new PasosRequisito();
                        pasoRequisito.Nombre = oModel.Nombre;
                        pasoRequisito.RequisitosId = oModel.requisitoId;
                        pasoRequisito.Estado = true;
                        _context.PasosRequisitos.Add(pasoRequisito);
                        _context.SaveChanges();
                       
                        transaction.Commit();
                        oresponse.success = 1;
                        oresponse.message = "Paso requisito registrado con exito";
                        oresponse.data = pasoRequisito;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                oresponse.message = ex.Message;
                return BadRequest(oresponse);
            }
            return Ok(oresponse);
        }
        [HttpPut("updatePasoRequisito/{id}")]
        public IActionResult updatePasoRequisito(pasos_update_request oModel, int id)
        {
            Response oresponse = new Response();
            try
            {
                var pasoRequisito = _context.PasosRequisitos.Find(id);
                if (pasoRequisito == null)
                {
                    oresponse.message = "La referencia no existe";
                    return Ok(oresponse);
                }
                pasoRequisito.Nombre = oModel.Nombre;
                pasoRequisito.Estado = true;
                _context.PasosRequisitos.Update(pasoRequisito);
                _context.SaveChanges();
              
                oresponse.success = 1;
                oresponse.message = "Paso requisito actualizado con exito";
                oresponse.data = pasoRequisito;
            }
            catch (Exception ex)
            {
                oresponse.message = ex.Message;
                return BadRequest(oresponse);
            }
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oresponse);
        }

        [HttpPut("deletePasoRequisito")]
        public IActionResult deletePasoRequisito(int id)
        {
            Response oResponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var pasosRequisito = _context.PasosRequisitos.Find(id);
                        if (pasosRequisito == null)
                        {
                            oResponse.message = "El paso requisito no existe";
                            throw new Exception();
                        }
                        if (pasosRequisito.Estado == false)
                        {
                            oResponse.message = "El paso requisito no existe";
                            throw new Exception();
                        }
                        pasosRequisito.Estado = false;
                        _context.PasosRequisitos.Update(pasosRequisito);
                        _context.SaveChanges();
                      
                        oResponse.success = 1;
                        oResponse.message = "El paso requisito fue eliminado con exito";
                        oResponse.data = pasosRequisito;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPut("restorePasoRequisito")]
        public IActionResult restorePasoRequisito(int id)
        {
            Response oResponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var pasoRequisito = _context.PasosRequisitos.Find(id);
                        if (pasoRequisito == null)
                        {
                            oResponse.message = "El paso requisito no existe";
                            throw new Exception();
                        }
                        if (pasoRequisito.Estado == true)
                        {
                            oResponse.message = "El paso requisito no esta eliminado";
                            throw new Exception();
                        }
                        pasoRequisito.Estado = true;
                        _context.PasosRequisitos.Update(pasoRequisito);
                        _context.SaveChanges();
                      
                        oResponse.success = 1;
                        oResponse.message = "El paso requisito fue restaurado con exito";
                        oResponse.data = pasoRequisito;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
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
