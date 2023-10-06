using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public UsuariosController (dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllUsers")]
        public IActionResult getAllUsers()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios;
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
    }
}
