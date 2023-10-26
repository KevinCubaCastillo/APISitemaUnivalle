using APISitemaUnivalle.Models.Response;
using APISitemaUnivalle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISitemaUnivalle.Models.Request.Categorias;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly dbUnivalleContext _context;
        public CategoriaController(dbUnivalleContext context)
        {
            _context = context;
        }

        [HttpGet("getAllCategorias")]
        public IActionResult getAllCategorias()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Categoria.Select(i => new
                {
                    idCategoria = i.IdCategoria,
                    nombreCategoria = i.NombreCategoria,
                    Descripcion = i.Descripcion,
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
        [HttpGet("getActiveCategorias")]
        public IActionResult getActiveCategorias()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Categoria.Where(i => i.Estado == true).Select(i => new
                {
                    idCategoria = i.IdCategoria,
                    nombreCategoria = i.NombreCategoria,
                    Descripcion = i.Descripcion,
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
        [HttpGet("getDeletedCategorias")]
        public IActionResult getDeletedCategorias()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Categoria.Where(i => i.Estado == false).Select(i => new
                {
                    idCategoria = i.IdCategoria,
                    nombreCategoria = i.NombreCategoria,
                    Descripcion = i.Descripcion,
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
        [HttpGet("getCategoriaById/{id}")]
        public IActionResult getCategoriaById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Categoria.Find(id);
                if (datos == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
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
        [HttpPost("addCategoria")]
        public IActionResult addCategoria(categorias_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var ver = _context.Categoria.FirstOrDefault(i => (i.NombreCategoria).ToUpper() == (oModel.NombreCategoria).ToUpper());
                if (ver != null)
                {
                    oResponse.message = "La categoria ya existe";
                    return BadRequest(oResponse);
                }
                Categorium categoria = new Categorium();
                categoria.NombreCategoria = oModel.NombreCategoria;
                categoria.Descripcion = oModel.Descripcion;
                categoria.Estado = true;
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Categoria registrado con exito";
                oResponse.data = categoria;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("updateCategoria/{id}")]
        public IActionResult updateModulo(categorias_update_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var categoria = _context.Categoria.Find(id);
                if (categoria == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                categoria.NombreCategoria = oModel.NombreCategoria;
                categoria.Descripcion = oModel.Descripcion;

                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Categoria actualizada con exito";
                oResponse.data = categoria;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("deleteCategoria/{id}")]
        public IActionResult deleteCategoria(int id)
        {
            Response oResponse = new Response();
            try
            {
                var categoria = _context.Categoria.Find(id);
                if (categoria == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                if (categoria.Estado == false)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                categoria.Estado = false;
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Categoria eliminada con exito";
                oResponse.data = categoria;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreCategoria/{id}")]
        public IActionResult restoreCategoria(int id)
        {
            Response oResponse = new Response();
            try
            {
                var categoria = _context.Categoria.Find(id);
                if (categoria == null)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                if (categoria.Estado == true)
                {
                    oResponse.message = "No se encontraron datos";
                    return NotFound(oResponse);
                }
                categoria.Estado = true;
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Categoria restaurada con exito";
                oResponse.data = categoria;
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
