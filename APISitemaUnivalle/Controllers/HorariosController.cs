using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Horarios;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public HorariosController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet ("getDias")]
        public IActionResult getDias()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Dias.Select(i => new
                {
                    i.IdDias,
                    i.NombreDia,
                    i.Estado
                });
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getHorario")]
        public IActionResult getHoras()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Horarios.Select(i => new
                {
                    i.IdHorarios,
                    i.HoraInicio,
                    i.HoraFin,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    servicio = i.IdServicioNavigation.Nombre,
                    i.Estado,
                    diasAtencion = i.Atencions.Select(a => new
                    {
                        a.IdAtencion,
                        a.IdDiaNavigation.NombreDia,
                    })
                });
                oResponse.success = 1;
                oResponse.data = datos;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPost("addHorario")]
        public IActionResult addHorario(horario_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Horario horario = new Horario();
                        horario.HoraInicio = oModel.HoraInicio;
                        horario.HoraFin = oModel.HoraFin;
                        horario.IdServicio = oModel.IdServicio;
                        horario.IdModulo = oModel.IdModulo;
                        horario.Estado = true;
                        _context.Horarios.Add(horario);
                        _context.SaveChanges();
                        foreach(var at in oModel.listAtencion)
                        {
                            Atencion atencion = new Atencion();
                            atencion.IdHorarios = horario.IdHorarios;
                            atencion.IdDia = at.IdDia;
                            atencion.Estado = true;
                            _context.Atencions.Add(atencion);
                            _context.SaveChanges();
                        }
                        oResponse.success = 1;
                        oResponse.message = "Horario registrado con exito";
                        oResponse.data = horario;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return BadRequest(oResponse);
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
