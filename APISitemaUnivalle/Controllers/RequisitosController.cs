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
                        foreach(var paso in oModel.pasos)
                        {
                            PasosRequisito pasosReq = new PasosRequisito();
                            pasosReq.Nombre = paso.Nombre;
                            pasosReq.RequisitosId = requisito.Id;
                            pasosReq.Estado = true;
                            _context.PasosRequisitos.Add(pasosReq);
                            _context.SaveChanges();
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
    }
}
