using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Requisitos;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public RequisitosController (dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllRequisitos")]
        public IActionResult getAllRequisitos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Requisitos.Select(i => new
                {
                    Identificador = i.Id,
                    descripcion = i.Descripcion,
                    servicio = i.Servicios.Nombre,
                    pasosRequisito = i.PasosRequisitos.Select(d => new
                    {
                        Identificador = d.Id,
                        d.Nombre,
                        Requisito = d.Requisitos.Descripcion,
                    })
                });
                if(datos.Count() == 0)
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
        [HttpGet("getActiveRequisitos")]
        public IActionResult getActiveRequisitos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Requisitos.Where(i => i.Estado == true).Select(i => new
                {
                    Identificador = i.Id,
                    descripcion = i.Descripcion,
                    servicio = i.Servicios.Nombre,
                    pasosRequisito = i.PasosRequisitos.Select(d => new
                    {
                        Identificador = d.Id,
                        d.Nombre,
                        Requisito = d.Requisitos.Descripcion,
                    })
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
        [HttpGet("getDeletedRequisitos")]
        public IActionResult getDeletedRequisitos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Requisitos.Where(i => i.Estado == false).Select(i => new
                {
                    Identificador = i.Id,
                    descripcion = i.Descripcion,
                    servicio = i.Servicios.Nombre,
                    pasosRequisito = i.PasosRequisitos.Select(d => new
                    {
                        Identificador = d.Id,
                        d.Nombre,
                        Requisito = d.Requisitos.Descripcion,
                    })
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
        [HttpGet("getRequisitosById/{id}")]
        public IActionResult getRequisitosById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Requisitos.Where(i => i.Estado == true && i.Id == id).Select(i => new
                {
                    Identificador = i.Id,
                    descripcion = i.Descripcion,
                    servicio = i.Servicios.Nombre,
                    pasosRequisito = i.PasosRequisitos.Select(d => new
                    {
                        Identificador = d.Id,
                        d.Nombre,
                        Requisito = d.Requisitos.Descripcion,
                    })
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
        [HttpPost("addRequisito")]
        public IActionResult addRequisito(requisito_add_request oModel)
        {
            Response oresponse = new Response();
            try
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Requisito requisito = new Requisito();
                        requisito.Descripcion = oModel.Descripcion;
                        requisito.ServiciosId = oModel.ServiciosId;
                        requisito.Estado = true;
                        _context.Requisitos.Add(requisito);
                        _context.SaveChanges();
                        if(oModel.pasos != null)
                        {
                            foreach (var paso in oModel.pasos)
                            {
                                PasosRequisito pasosReq = new PasosRequisito();
                                pasosReq.Nombre = paso.Nombre;
                                pasosReq.RequisitosId = requisito.Id;
                                pasosReq.Estado = true;
                                _context.PasosRequisitos.Add(pasosReq);
                                _context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                        oresponse.success = 1;
                        oresponse.message = "Requisito registrado con exito";
                        oresponse.data = requisito;
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
        [HttpPut("deleteRequisito")]
        public IActionResult deleteRequisito(int id)
        {
            Response oResponse = new Response();
            try
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var requisito = _context.Requisitos.Find(id);
                        if(requisito == null)
                        {
                            oResponse.message = "El requisito no existe";
                            throw new Exception();
                        }
                        if(requisito.Estado == false)
                        {
                            oResponse.message = "El requisito no existe";
                            throw new Exception();
                        }
                        requisito.Estado = false;
                        _context.Requisitos.Update(requisito);
                        _context.SaveChanges();
                        foreach(var paso in requisito.PasosRequisitos)
                        {
                            paso.Estado = false;
                            _context.PasosRequisitos.Update(paso);
                            _context.SaveChanges();
                        }
                        oResponse.success = 1;
                        oResponse.message = "Requisito eliminado con exito";
                        oResponse.data = requisito;
                        transaction.Commit();
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreRequisito")]
        public IActionResult restoreRequisito(int id)
        {
            Response oResponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var requisito = _context.Requisitos.Find(id);
                        if (requisito == null)
                        {
                            oResponse.message = "El requisito no existe";
                            throw new Exception();
                        }
                        if (requisito.Estado == true)
                        {
                            oResponse.message = "El requisito no esta eliminado";
                            throw new Exception();
                        }
                        requisito.Estado = true;
                        _context.Requisitos.Update(requisito);
                        _context.SaveChanges();
                        foreach (var paso in requisito.PasosRequisitos)
                        {
                            paso.Estado = true;
                            _context.PasosRequisitos.Update(paso);
                            _context.SaveChanges();
                        }
                        oResponse.success = 1;
                        oResponse.message = "Requisito restaurado con exito";
                        oResponse.data = requisito;
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
