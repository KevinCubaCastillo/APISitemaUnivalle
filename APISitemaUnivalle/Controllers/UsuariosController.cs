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
                var datos = _context.Usuarios.Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
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
        [HttpGet("getActiveUsers")]
        public IActionResult getActiveUsers()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios.Where(i => i.Estado).Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
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
        [HttpGet("getDisabledUsers")]
        public IActionResult getDisabledUsers()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios.Where(i => !i.Estado).Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
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
        [HttpGet("getUserById/{id}")]
        public IActionResult getUserById(string id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.CiUsuario == id && i.Estado).Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getUserByCargoId/{id}")]
        public IActionResult getUserByCargoId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.CargoId == id).Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getActiveUsersByCargoId/{id}")]
        public IActionResult getActiveUserByCargoId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.CargoId == id && i.Estado).Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getDisabledUsersByCargoId/{id}")]
        public IActionResult getDisabledUserByCargoId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Where(i => i.CargoId == id && !i.Estado).Select(i => new
                {
                    CI = i.CiUsuario,
                    i.Nombres,
                    i.Apellidos,
                    cargo = i.Cargo.Nombrecargo,
                    i.Estado
                });
                if (user.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = user;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPost("addUser")]
        public IActionResult addUser(usuario_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verf = _context.Usuarios.Find(oModel.CiUsuario);
                if(verf != null)
                {
                    oResponse.message = "El usuario ya se encuentra registrado.";
                    return BadRequest(oResponse);
                }
                Usuario user = new Usuario();
                user.CiUsuario = oModel.CiUsuario;
                user.Clave = Encrypt.GetSHA256(oModel.Clave);
                user.Nombres = oModel.Nombres;
                user.Apellidos = oModel.Apellidos;
                user.CargoId = oModel.CargoId;
                user.Estado = true;
                _context.Usuarios.Add(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario registrado con exito";
                oResponse.data = user;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateUser/{ci}")]
        public IActionResult updateUser(usuario_update_request oModel, string ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Find(ci);
                if (user == null)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                user.Nombres = oModel.Nombres;
                user.Apellidos = oModel.Apellidos;
                _context.Usuarios.Update(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario actualizado con exito";
                oResponse.data = user;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteUser/{ci}")]
        public IActionResult deleteUser(string ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Find(ci);
                if (user == null)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                if (!user.Estado)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                user.Estado = false;
                _context.Usuarios.Update(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario eliminado con exito";
                oResponse.data = user;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreUser/{ci}")]
        public IActionResult restoreUser(string ci)
        {
            Response oResponse = new Response();
            try
            {
                var user = _context.Usuarios.Find(ci);
                if (user == null)
                {
                    oResponse.message = "El usuario no existe.";
                    return BadRequest(oResponse);
                }
                if (user.Estado)
                {
                    oResponse.message = "El usuario no esta eliminado.";
                    return BadRequest(oResponse);
                }
                user.Estado = true;
                _context.Usuarios.Update(user);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Usuario restaurado con exito";
                oResponse.data = user;
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
