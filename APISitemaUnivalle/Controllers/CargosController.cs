using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Cargos;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public CargosController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet ("getAllCargos")]
        public IActionResult getAllCargos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Cargos.Select(c => new
                {
                    c.Id,
                    c.Nombrecargo,
                    c.Estado
                });
                if(datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
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
        [HttpGet ("getActiveCargos")]
        public IActionResult getActiveCargos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Cargos.Where(i => i.Estado).Select(c => new
                {
                    c.Id,
                    c.Nombrecargo,
                    c.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
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

        [HttpGet("getDisabledCargos")]
        public IActionResult getDisabledCargos()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Cargos.Where(i => !i.Estado).Select(c => new
                {
                    c.Id,
                    c.Nombrecargo,
                    c.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
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
        [HttpGet("getCargoById/{id}")]
        public IActionResult getCargoById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Cargos.Where(i => i.Id == id && !i.Estado).Select(c => new
                {
                    c.Id,
                    c.Nombrecargo,
                    c.Estado
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
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
        [HttpPost("addCargo")]
        public IActionResult addCargo(cargo_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Cargos.Where(i => i.Nombrecargo.ToUpper() == oModel.Nombrecargo.ToUpper());
                if(verf.Count() != 0)
                {
                    oResponse.message = "El cargo ya existe";
                    return BadRequest(oResponse);
                }
                Cargo cargo = new Cargo();
                cargo.Nombrecargo = oModel.Nombrecargo;
                cargo.Estado = true;
                _context.Cargos.Add(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Cargo registrado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateCargo/{id}")]
        public IActionResult updateCargo(cargo_add_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var cargo = _context.Cargos.Find(id);
                if (cargo == null)
                {
                    oResponse.message = "El cargo no existe";
                    return BadRequest(oResponse);
                }
                cargo.Nombrecargo = oModel.Nombrecargo;
                _context.Cargos.Update(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Cargo actualizado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteCargo/{id}")]
        public IActionResult deleteCargo(int id)
        {
            Response oResponse = new Response();
            try
            {
                var cargo = _context.Cargos.Find(id);
                if (cargo == null)
                {
                    oResponse.message = "El cargo no existe";
                    return BadRequest(oResponse);
                }
                if (!cargo.Estado)
                {
                    oResponse.message = "El cargo no existe";
                    return BadRequest(oResponse);
                }
                cargo.Estado = false;
                _context.Cargos.Update(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Cargo eliminado con exito";
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreCargo/{id}")]
        public IActionResult restoreCargo(int id)
        {
            Response oResponse = new Response();
            try
            {
                var cargo = _context.Cargos.Find(id);
                if (cargo == null)
                {
                    oResponse.message = "El cargo no existe";
                    return BadRequest(oResponse);
                }
                if (cargo.Estado)
                {
                    oResponse.message = "El cargo no esta eliminado";
                    return BadRequest(oResponse);
                }
                cargo.Estado = true;
                _context.Cargos.Update(cargo);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.data = cargo;
                oResponse.message = "Cargo restaurado con exito";
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
