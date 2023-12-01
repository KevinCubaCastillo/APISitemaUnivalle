using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Servicios;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.AccessControl;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public ServiciosController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllServicios")]
        public IActionResult getAllServicios()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    Categoria = i.IdCategoriaNavigation.Descripcion,
                    imagen = i.ImagenUrl,
                    i.Estado
                });
                if(datos.Count() == 0)
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
        [HttpGet("getActiveServicios")]
        public IActionResult getActiveServicios()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == true).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl,
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
        [HttpGet("getDeletedServicios")]
        public IActionResult getDeletedServicios()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == false).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl,
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
        [HttpGet("getServicioById/{id}")]
        public IActionResult getServicioById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == true).Include(e => e.Ubicaciones).Include(e => e.Referencia).Where(e=> e.Id == id);
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

        [HttpGet("getTramiteById/{id}")]
        public IActionResult getTramiteById(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == true).Include(e => e.Ubicaciones).Include(e => e.Referencia).Include(e => e.Tramites).Include(e => e.Requisitos).Where(e => e.Id == id);
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
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }

        [HttpGet("getServicioByModule/{name}")]
        public IActionResult getServicioByModule(string name)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Include(e => e.Ubicaciones).Include(e => e.Referencia).Where(e => e.Modulo.Nombremodulo.Equals(name));
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


        [HttpGet("getTramiteByModuleActive/{name}")]
        public IActionResult getTramiteByModuleActive(string name)
        {
            Response oResponse = new Response();
            try
            {
              //   var datos = _context.Servicios.Where(i => i.Estado == true).Include(e => e.Ubicaciones).Include(e => e.Referencia).Include(e => e.Tramites).Where(e => e.Modulo.Nombremodulo.Equals(name));
              
                var datos = _context.Servicios.Where(i => i.Estado == true).Where(e => e.Modulo.Nombremodulo.Equals(name)).Select(i => new
                {
                    id = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    Categoria = i.IdCategoriaNavigation.NombreCategoria,
                    imagen = i.ImagenUrl,
                    i.Estado,
                    Ubicaciones = i.Ubicaciones,
                    Referencia = i.Referencia,
                    Tramites = i.Tramites
                });
                
                
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
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }

        [HttpGet("getTramiteByModuleInactive/{name}")]
        public IActionResult getTramiteByModuleInactive(string name)
        {
            Response oResponse = new Response();
            try
            {
             //   var datos = _context.Servicios.Where(i => i.Estado == false).Include(e => e.Ubicaciones).Include(e => e.Referencia).Include(e => e.Tramites).Include(e => e.IdCategoriaNavigation.Descripcion).Where(e => e.Modulo.Nombremodulo.Equals(name));
                var datos = _context.Servicios.Where(i => i.Estado == false).Where(e => e.Modulo.Nombremodulo.Equals(name)).Select(i => new
                {
                    id = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    Categoria = i.IdCategoriaNavigation.NombreCategoria,
                    imagen = i.ImagenUrl,
                    i.Estado,
                    Ubicaciones = i.Ubicaciones,
                    Referencia = i.Referencia,
                    Tramites = i.Tramites
                });
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
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }
        [HttpGet("getTramiteByCategory/{name}")]
        public IActionResult getTramiteByNameCategory(string name)
        {
            Response oResponse = new Response();
            try
            {
                //   var datos = _context.Servicios.Where(i => i.Estado == false).Include(e => e.Ubicaciones).Include(e => e.Referencia).Include(e => e.Tramites).Include(e => e.IdCategoriaNavigation.Descripcion).Where(e => e.Modulo.Nombremodulo.Equals(name));
              
                var datos = _context.Servicios.Where(i => i.Estado == true).Where(e => e.IdCategoriaNavigation.NombreCategoria.Equals(name)).Select(i => new
                {
                    id = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    Categoria = i.IdCategoriaNavigation.NombreCategoria,
                    imagen = i.ImagenUrl,
                    i.Estado,
                    Ubicaciones = i.Ubicaciones,
                    Referencia = i.Referencia,
                    Tramites = i.Tramites
                });
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
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(oResponse);
        }
        [HttpGet("getServicioByModuloId/{id}")]
        public IActionResult getServicioByModuloId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == true && i.ModuloId == id).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl,
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
        [HttpGet("getDisabledServicioByModuloId/{id}")]
        public IActionResult getDisabledServicioByModuloId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.Estado == false && i.ModuloId == id).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl,
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
        [HttpGet("getAllServiciosByModuloId/{id}")]
        public IActionResult getAllServiciosByModuloId(int id)
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Servicios.Where(i => i.ModuloId == id).Select(i => new
                {
                    identificador = i.Id,
                    nombre = i.Nombre,
                    modulo = i.Modulo.Nombremodulo,
                    imagen = i.ImagenUrl,
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
        [HttpPost ("addServicio")]
        public IActionResult addServicio(servicio_add_request oModel)
        {
            Response oResponse = new Response();
            try
            {
                var verify = _context.Servicios.FirstOrDefault(i => (i.Nombre).ToUpper() == (oModel.Nombre).ToUpper() && i.ModuloId == oModel.ModuloId);
                if(verify != null)
                {
                    oResponse.message = "El servicio ya existe";
                    return BadRequest(oResponse);
                }
                Servicio servicio = new Servicio();
                servicio.Nombre = oModel.Nombre;
                servicio.ModuloId = oModel.ModuloId;
                servicio.ImagenUrl = oModel.ImagenUrl;
                servicio.IdCategoria = oModel.IdCategoria;
                servicio.Estado = true;
                _context.Servicios.Add(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio registrado con exito";
                oResponse.data = servicio;
            }
            catch(Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPost("addServicioWDetails")]
        public IActionResult addServicioWDetails(servicio_add_request_all oModel)
        {
            Response oResponse = new Response();
            try
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var verify = _context.Servicios.FirstOrDefault(i => (i.Nombre).ToUpper() == (oModel.Nombre).ToUpper() && i.ModuloId == oModel.ModuloId);
                        if (verify != null)
                        {
                            oResponse.message = "El servicio ya existe";
                            throw new Exception();
                        }
                        Servicio servicio = new Servicio();
                        servicio.Nombre = oModel.Nombre;
                        servicio.ModuloId = oModel.ModuloId;
                        servicio.ImagenUrl = oModel.ImagenUrl;
                        servicio.Estado = true;
                        _context.Servicios.Add(servicio);
                        _context.SaveChanges();
                        if (oModel.referenciaAdd != null)
                        {
                            Referencium referencia = new Referencium();
                            referencia.Nombre = oModel.referenciaAdd.Nombre;
                            referencia.Numerocel = oModel.referenciaAdd.Numerocel;
                            referencia.ServiciosId = servicio.Id;
                            referencia.Estado = true;
                            _context.Referencia.Add(referencia);
                            _context.SaveChanges();
                        }
                        if (oModel.ubicacionAdd != null)
                        {
                            Ubicacione ubicacion = new Ubicacione();
                            ubicacion.Descripcion = oModel.ubicacionAdd.Descripcion;
                            ubicacion.Imagen = oModel.ubicacionAdd.Imagen;
                            ubicacion.Video = oModel.ubicacionAdd.Video;
                            ubicacion.ServiciosId = servicio.Id;
                            ubicacion.Estado = true;
                            _context.Ubicaciones.Add(ubicacion);
                            _context.SaveChanges();
                        }
                        if (oModel.carreraAdd != null)
                        {
                            Carrera carrera = new Carrera();
                            carrera.Nombre = oModel.carreraAdd.Nombre;
                            carrera.ServiciosId = servicio.Id;
                            carrera.Estado = true;
                            _context.Carreras.Add(carrera);
                            _context.SaveChanges();
                        }
                        if (oModel.requisitoAdd != null)
                        {
                            Requisito requisito = new Requisito();
                            requisito.Descripcion = oModel.requisitoAdd.Descripcion;
                            requisito.ServiciosId = servicio.Id;
                            requisito.Estado = true;
                            _context.Requisitos.Add(requisito);
                            _context.SaveChanges();
                            if (oModel.requisitoAdd.pasos != null)
                            {
                                foreach (var paso in oModel.requisitoAdd.pasos)
                                {
                                    PasosRequisito pasosReq = new PasosRequisito();
                                    pasosReq.Nombre = paso.Nombre;
                                    pasosReq.RequisitosId = requisito.Id;
                                    pasosReq.Estado = true;
                                    _context.PasosRequisitos.Add(pasosReq);
                                    _context.SaveChanges();
                                }
                            }
                        }
                        transaction.Commit();
                        oResponse.success = 1;
                        oResponse.message = "Servicio registrado con exito";
                        oResponse.data = servicio;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        oResponse.message = ex.Message;
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

        [HttpPost("addTramite")]
        public IActionResult addTramite(tramite_add_request_all oModel)
        {
            Response oResponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var verify = _context.Servicios.FirstOrDefault(i => (i.Nombre).ToUpper() == (oModel.Nombre).ToUpper() && i.ModuloId == oModel.ModuloId);
                        if (verify != null)
                        {
                            oResponse.message = "El tramite ya existe";
                            throw new Exception();
                        }
                        Servicio servicio = new Servicio();
                        servicio.Nombre = oModel.Nombre;
                        servicio.ModuloId = oModel.ModuloId;
                        servicio.ImagenUrl = oModel.ImagenUrl;
                        servicio.IdCategoria = oModel.IdCategoria;
                        servicio.Estado = true;
                        _context.Servicios.Add(servicio);
                        _context.SaveChanges();
                        if (oModel.referenciaAdd != null)
                        {
                            Referencium referencia = new Referencium();
                            referencia.Nombre = oModel.referenciaAdd.Nombre;
                            referencia.Numerocel = oModel.referenciaAdd.Numerocel;
                            referencia.ServiciosId = servicio.Id;
                            referencia.Estado = true;
                            _context.Referencia.Add(referencia);
                            _context.SaveChanges();
                        }
                        if(oModel.duracionAdd != null)
                        {
                            Tramite tramite = new Tramite();
                            tramite.Tiempotramite = oModel.duracionAdd.Tiempotramite;
                            tramite.ServiciosId = servicio.Id;
                            tramite.Estado = true;
                            _context.Tramites.Add(tramite);
                            _context.SaveChanges();
                            oResponse.success = 1;
                            oResponse.message = "Tramite registrado con exito";
                            oResponse.data = tramite;

                          
                        }
                        if (oModel.ubicacionAdd != null)
                        {
                            Ubicacione ubicacion = new Ubicacione();
                            ubicacion.Descripcion = oModel.ubicacionAdd.Descripcion;
                            ubicacion.Imagen = oModel.ubicacionAdd.Imagen;
                            ubicacion.Video = oModel.ubicacionAdd.Video;
                            ubicacion.ServiciosId = servicio.Id;
                            ubicacion.Estado = true;
                            _context.Ubicaciones.Add(ubicacion);
                            _context.SaveChanges();
                        }
    
                        if (oModel.requisitoAdd != null)
                        {

                            Requisito requisito = new Requisito();
                            requisito.Descripcion = oModel.requisitoAdd.Descripcion;
                            requisito.ServiciosId = servicio.Id;
                            requisito.Estado = true;
                            _context.Requisitos.Add(requisito);
                            _context.SaveChanges();
                            if (oModel.requisitoAdd.pasos != null)
                            {
                                foreach (var paso in oModel.requisitoAdd.pasos)
                                {
                                    PasosRequisito pasosReq = new PasosRequisito();
                                    pasosReq.Nombre = paso.Nombre;
                                    pasosReq.RequisitosId = requisito.Id;
                                    pasosReq.Estado = true;
                                    _context.PasosRequisitos.Add(pasosReq);
                                    _context.SaveChanges();
                                }
                            }
                        }
                        transaction.Commit();
                        oResponse.success = 1;
                        oResponse.message = "Tramite registrado con exito";
                        oResponse.data = servicio;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        oResponse.message = ex.Message;
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
        [HttpPut("updateServicio/{id}")]
        public IActionResult updateServicio(servicio_update_request oModel, int id)
        {
            Response oResponse = new Response();
            try
            {
                var servicio = _context.Servicios.Find(id);
                if (servicio == null)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                servicio.Nombre = oModel.Nombre;
                servicio.ImagenUrl = oModel.ImagenUrl;
                servicio.IdCategoria = oModel.IdCategoria;
                servicio.Estado = true;
                _context.Servicios.Update(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio actualizado con exito";
                oResponse.data = servicio;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }

        [HttpPut("deleteServicio/{id}")]
        public IActionResult deleteServicio(int id)
        {
            Response oResponse = new Response();
            try
            {
                var servicio = _context.Servicios.Find(id);
                if (servicio == null)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                if(servicio.Estado == false)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                servicio.Estado = false;
                _context.Servicios.Update(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio eliminado con exito";
                oResponse.data = servicio;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpPut("restoreServicio/{id}")]
        public IActionResult restoreServicio(int id)
        {
            Response oResponse = new Response();
            try
            {
                var servicio = _context.Servicios.Find(id);
                if (servicio == null)
                {
                    oResponse.message = "El servicio no existe";
                    return BadRequest(oResponse);
                }
                if (servicio.Estado == true)
                {
                    oResponse.message = "El servicio no esta eliminado";
                    return BadRequest(oResponse);
                }
                servicio.Estado = true;
                _context.Servicios.Update(servicio);
                _context.SaveChanges();
                oResponse.success = 1;
                oResponse.message = "Servicio restaurado con exito";
                oResponse.data = servicio;
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
