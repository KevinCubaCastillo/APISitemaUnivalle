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






        [HttpGet("getHorariosbyServicioId/{id}")]
        public IActionResult getHorariosbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Horarios.Where(r => r.Estado == true && r.IdServicio == id).Select(i => new
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

        [HttpGet("getDisabledHorariosbyServicioId/{id}")]
        public IActionResult getDisabledHorariosbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Horarios.Where(r => r.Estado == false && r.IdServicio == id).Select(i => new
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
        [HttpGet("getAllHorariosbyServicioId/{id}")]
        public IActionResult getAllHorariosbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Horarios.Where(r => r.IdServicio == id).Select(i => new
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

        [HttpPut("deleteHorarios/{id}")]
        public IActionResult deleteHorarios(int id)
        {
            Response oResponse = new Response();
            try
            {
                var horario = _context.Horarios.Find(id);
                if (horario == null)
                {
                    oResponse.message = "El horario no existe";
                    return BadRequest(oResponse);
                }
                if (!horario.Estado)
                {
                    oResponse.message = "El horario no existe";
                    return BadRequest(oResponse);
                }
                horario.Estado = false;
                _context.Horarios.Update(horario);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = horario;
                oResponse.message = "Horario eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPut("restoreHorarios/{id}")]
        public IActionResult restoreHorarios(int id)
        {
            Response oResponse = new Response();
            try
            {
                var horario = _context.Horarios.Find(id);
                if (horario == null)
                {
                    oResponse.message = "El horario no existe";
                    return BadRequest(oResponse);
                }
                if (horario.Estado)
                {
                    oResponse.message = "El horario no esta eliminado";
                    return BadRequest(oResponse);
                }
                horario.Estado = true;
                _context.Horarios.Update(horario);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = horario;
                oResponse.message = "Horario restaurado con exito";
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
