using APISitemaUnivalle.Models;
using APISitemaUnivalle.Models.Request.Publicacion;
using APISitemaUnivalle.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISitemaUnivalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly dbUnivalleContext _context;
        public PublicacionesController(dbUnivalleContext context)
        {
            _context = context;
        }
        [HttpGet("getAllPublications")]
        public IActionResult getAllPublications()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Publicacions.Select( i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
                    })
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron datos validos";
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
        [HttpGet("GetActivePublicaciones")]
        public IActionResult GetActivePublicaciones()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Publicacions.Where(p => p.Estado).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
                    })
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron publicaciones activas";
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
        [HttpGet("GetInactivePublicaciones")]
        public IActionResult GetInactivePublicaciones()
        {
            Response oResponse = new Response();
            try
            {
                var datos = _context.Publicacions.Where(p => !p.Estado).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
                    })
                });
                if (datos.Count() == 0)
                {
                    oResponse.message = "No se encontraron publicaciones inactivas";
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
        [HttpGet("GetPublicacionByID/{id}")]
        public IActionResult GetPublicacionByID(int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Where(p => p.Id == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
                    })
                });

                if (publicacion == null)
                {
                    oResponse.message = "No se encontró la publicación con el ID proporcionado";
                    return BadRequest(oResponse);
                }

                oResponse.data = publicacion;
                oResponse.success = 1;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
                return BadRequest(oResponse);
            }
            return Ok(oResponse);
        }
        [HttpGet("getPublicacionesbyServicioId/{id}")]
        public IActionResult getPublicacionesbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Publicacions.Where(r => r.Estado == true && r.ServiciosId == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
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
        [HttpGet("getDisabledPublicacionesbyServicioId/{id}")]
        public IActionResult getDisabledPublicacionesbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Publicacions.Where(r => r.Estado == false && r.ServiciosId == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
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
        [HttpGet("getAllPublicacionesbyServicioId/{id}")]
        public IActionResult getAllPublicacionesbyServicioId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Publicacions.Where(r => r.ServiciosId == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
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
        [HttpGet("getPublicacionesbyModuloId/{id}")]
        public IActionResult getPublicacionesbyModuloId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Publicacions.Where(r => r.Estado == true && r.IdModulo == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
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
        [HttpGet("getDisabledPublicacionesbyModuloId/{id}")]
        public IActionResult getDisabledPublicacionesbyModuloId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Publicacions.Where(r => r.Estado == false && r.IdModulo == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
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
        [HttpGet("getAllPublicacionesbyModuloId/{id}")]
        public IActionResult getAllPublicacionesbyModuloId(int id)
        {
            Response oResponse = new Response();
            try
            {

                var datos = _context.Publicacions.Where(r => r.IdModulo == id).Select(i => new
                {
                    Identificador = i.Id,
                    i.Archivo,
                    i.Titulo,
                    servicio = i.Servicios.Nombre,
                    modulo = i.IdModuloNavigation.Nombremodulo,
                    i.Estado,
                    descripcion = i.DescripcionPublicacions.Select(d => new
                    {
                        d.IdDescripcion,
                        d.Contenido
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
        [HttpPost("AddPublicaciones")]
        public IActionResult addPublicacion(Publicacion_add_Request oPublicacion)
        {
            Response oResponse = new Response();
            try
            {
                
                Publicacion npublicacion = new Publicacion();
                //npublicacion.Id = oPublicacion.Id;
                npublicacion.Archivo = oPublicacion.Archivo;
                npublicacion.ServiciosId = oPublicacion.ServiciosId;
                npublicacion.IdModulo = oPublicacion.id_modulo;
                npublicacion.Titulo = oPublicacion.Titulo;
                npublicacion.Estado = oPublicacion.Estado;
                _context.Publicacions.Add(npublicacion);
                _context.SaveChanges();
                oResponse.message = "agregado con exito";
                oResponse.success = 1;
                oResponse.data = npublicacion;
                return Ok(oResponse);
            }

            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);

        }
        [HttpPost("addPublicacionWithDescription")]
        public IActionResult addPublicacionWithDescription(Publicacion_add_Request oPublicacion)
        {
            Response oResponse = new Response();
            try
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Publicacion npublicacion = new Publicacion();
                        //npublicacion.Id = oPublicacion.Id;
                        npublicacion.Archivo = oPublicacion.Archivo;
                        npublicacion.ServiciosId = oPublicacion.ServiciosId;
                        npublicacion.IdModulo = oPublicacion.id_modulo;
                        npublicacion.Titulo = oPublicacion.Titulo;
                        npublicacion.Estado = oPublicacion.Estado;
                        _context.Publicacions.Add(npublicacion);
                        _context.SaveChanges();
                        if(oPublicacion.descripcionPublicacion != null)
                        {
                            foreach (var des in oPublicacion.descripcionPublicacion)
                            {
                                DescripcionPublicacion nDes = new DescripcionPublicacion();
                                nDes.IdPublicacion = npublicacion.Id;
                                nDes.Contenido = des.Contenido;
                                nDes.Estado = true;
                                _context.DescripcionPublicacions.Add(nDes);
                                _context.SaveChanges();
                            }
                        }
                        oResponse.message = "Agregado con exito";
                        oResponse.success = 1;
                        oResponse.data = npublicacion;
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        oResponse.message = ex.InnerException.Message;
                    }
                }

            }

            catch (Exception ex)
            {
                oResponse.message = ex.InnerException.Message;
            }
            return Ok(oResponse);

        }
        [HttpPut("UpdatePublicaciones/{id}")]
        public IActionResult UpdatePublicaciones(Publicacion_edit_Request oPublicacion, int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Find(id);
                if (publicacion == null)
                {
                    oResponse.message = "El cliente no existe";
                    return Ok(oResponse);
                }
                publicacion.Archivo = oPublicacion.Archivo;
                publicacion.Titulo = oPublicacion.Titulo;
                _context.Publicacions.Update(publicacion);
                _context.SaveChanges();
                oResponse.message = "editado con exito";
                oResponse.success = 1;
                oResponse.data = publicacion;
                return Ok(oResponse);


            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }

        [HttpPut("UpdatePublicacionesWithDescription/{id}")]
        public IActionResult UpdatePublicacionesWithDescription(Publicacion_edit_Request oPublicacion, int id)
        {
            Response oResponse = new Response();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var publicacion = _context.Publicacions.Find(id);
                        if (publicacion == null)
                        {
                            oResponse.message = "El cliente no existe";
                            throw new Exception();
                        }
                        publicacion.Archivo = oPublicacion.Archivo;
                        publicacion.Titulo = oPublicacion.Titulo;
                        _context.Publicacions.Update(publicacion);
                        _context.SaveChanges();
                        if (oPublicacion.descripcionPublicacion != null)
                        {
                            foreach (var des in oPublicacion.descripcionPublicacion)
                            {
                                var descripcion = _context.DescripcionPublicacions.Find(des.Id);
                                if (descripcion == null)
                                {
                                    throw new Exception();
                                }
                                descripcion.Contenido = des.Contenido;
                                _context.DescripcionPublicacions.Update(descripcion);
                                _context.SaveChanges();
                            }
                        }
                        oResponse.message = "Editado con exito";
                        oResponse.success = 1;
                        oResponse.data = publicacion;
                        transaction.Commit();
                        return Ok(oResponse);
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
            }
            return Ok(oResponse);
        }

        [HttpPut("DeletePublicaciones")]
        public IActionResult DeletePublicaciones(int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Find(id);
                if (publicacion == null)
                {
                    oResponse.message = "La publicación no existe";
                    return Ok(oResponse);
                }
                if (publicacion.Estado == false)
                {
                    oResponse.message = "La publicacion no existe";
                    return Ok(oResponse);
                }
                publicacion.Estado = false;
                _context.Publicacions.Update(publicacion);
                _context.SaveChanges();

                oResponse.message = "La publicación ha sido desactivada";
                oResponse.success = 1;
                oResponse.data = publicacion;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }



        [HttpPut("RestorePublicaciones")]
        public IActionResult ActivarPublicacion(int id)
        {
            Response oResponse = new Response();
            try
            {
                var publicacion = _context.Publicacions.Find(id);
                if (publicacion == null)
                {
                    oResponse.message = "La publicación no existe";
                    return Ok(oResponse);
                }
                if (publicacion.Estado == true)
                {
                    oResponse.message = "La publicacion ya existe";
                    return Ok(oResponse);
                }
                publicacion.Estado = true;
                _context.Publicacions.Update(publicacion);
                _context.SaveChanges();

                oResponse.message = "La publicación ha sido activada";
                oResponse.success = 1;
                oResponse.data = publicacion;
            }
            catch (Exception ex)
            {
                oResponse.message = ex.Message;
            }
            return Ok(oResponse);
        }


    }
}
