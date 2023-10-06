using APISitemaUnivalle.Models;
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
    }
}
