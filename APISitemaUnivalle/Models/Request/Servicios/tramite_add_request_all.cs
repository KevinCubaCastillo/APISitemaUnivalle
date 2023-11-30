using APISitemaUnivalle.Models.Request.Carreras;
using APISitemaUnivalle.Models.Request.Referencias;
using APISitemaUnivalle.Models.Request.Requisitos;
using APISitemaUnivalle.Models.Request.Tramites;
using APISitemaUnivalle.Models.Request.Ubicacion;
using APISitemaUnivalle.Models.Request.Tramites;

namespace APISitemaUnivalle.Models.Request.Servicios
{
    public class tramite_add_request_all
    {
        public string Nombre { get; set; } = null!;
        public int ModuloId { get; set; }
        public string? ImagenUrl { get; set; }
        public int? IdCategoria { get; set; }

        public Referencias_add_Request? referenciaAdd { get; set; }
        public tramite_add_request? duracionAdd { get; set; }
        public requisito_add_request? requisitoAdd { get; set; }
        public ubicacion_add_request? ubicacionAdd { get; set; }      
    }
}
