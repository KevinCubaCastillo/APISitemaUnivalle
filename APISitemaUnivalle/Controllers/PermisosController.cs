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
    public class PermisosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public PermisosController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllPermisosUsuarios")]
        public IActionResult getPermisosUsuarios ()
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
                    i.Estado,
                    modulos = i.UsuarioModulos.Select(d => new
                    {
                        d.Id,
                        modulo = d.IdModuloNavigation.Nombremodulo,
                        
                    })
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
        [HttpGet("getActivePermisosUsuarios")]
        public IActionResult getActivePermisosUsuarios()
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
                    i.Estado,
                    modulos = i.UsuarioModulos.Select(d => new
                    {
                        d.Id,
                        modulo = d.IdModuloNavigation.Nombremodulo,
                    })
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
        [HttpGet("getDisablePermisosUsuarios")]
        public IActionResult getDisableActivePermisosUsuarios()
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
                    i.Estado,
                    modulos = i.UsuarioModulos.Select(d => new
                    {
                        d.Id,
                        modulo = d.IdModuloNavigation.Nombremodulo,
                    })
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
        [HttpGet("getModulosByUserCI/{ci}")]
        public IActionResult getModulosByUserCI(string ci)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Usuarios.Where(i => i.Estado && i.CiUsuario == ci).Select(i => new
                {
                    modulos = i.UsuarioModulos.Where(d => d.IdModuloNavigation.Estado).Select(d => new
                    {
                        d.Id,
                        modulo = d.IdModuloNavigation.Nombremodulo,
                    })
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos";
                    return BadRequest(oResponse);
                }
                oResponse.data = datos;
                oResponse.success = 1;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("addPermisosUsuario/{ci}")]
        public IActionResult addPersmisosUsuario(permisos_add_request oModel ,string ci)
        {
            Response oResponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var user = _context.Usuarios.Find(ci);
                        if (user == null)
                        {
                            oResponse.message = "El usuario no existe.";
                            return BadRequest(oResponse);
                        }
                        foreach (var modulo in oModel.listModulo)
                        {
                            UsuarioModulo usuarioModulo = new UsuarioModulo();
                            usuarioModulo.CiUsuario = user.CiUsuario;
                            usuarioModulo.IdModulo = modulo.id_modulo;
                            _context.UsuarioModulos.Add(usuarioModulo);
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                        oResponse.success = 1;
                        oResponse.message = "Permisos registrados con exito";
                        oResponse.data = user;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        oResponse.message = ex.Message;
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
        [HttpDelete("deletePermiso/{id}")]
        public IActionResult deletePermiso(int id)
        {
            Response oResponse = new Response();
            try
            {
                var permiso = _context.UsuarioModulos.Find(id);
                if(permiso == null)
                {
                    oResponse.message = "El permiso no existe";
                    return NotFound(oResponse);
                }
                _context.UsuarioModulos.Remove(permiso);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Permiso eliminado con exito.";
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
    }
}
