using APISitemaUnivalle.Models.Request.Carreras;
using APISitemaUnivalle.Models.Request.Referencias;
using APISitemaUnivalle.Models.Request.Requisitos;
using APISitemaUnivalle.Models.Request.Ubicacion;

namespace APISitemaUnivalle.Models.Request.Servicios
{
    public class servicio_add_request_all
    {
        public string Nombre { get; set; } = null!;
        public int ModuloId { get; set; }
        public string? ImagenUrl { get; set; }
        public ubicacion_add_request? ubicacionAdd { get; set; }
        public Referencias_add_Request? referenciaAdd { get; set; }
        public requisito_add_request? requisitoAdd { get; set; }
        public carrera_add_request? carreraAdd { get; set; }

    }
}
