using apiPlanetFitness.Models.Tools;
using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Usuario;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public LoginController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult login(usuario_login_request auth)
        {
            Response oResponse = new Response();
            try
            {
                var userSession = _context.Usuarios.Where(i => i.CiUsuario == auth.CiUsuario && i.Clave == Encrypt.GetSHA256(auth.Clave) && i.Estado).Select(i => new
                {
                    i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    i.Estado
                });
                if(userSession.Count() == 0)
                {
                    oResponse.message = "Error al ingresar, verifique sus datos.";
                    return NotFound(oResponse);
                }
                oResponse.success = 1;
                oResponse.data = userSession;
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
