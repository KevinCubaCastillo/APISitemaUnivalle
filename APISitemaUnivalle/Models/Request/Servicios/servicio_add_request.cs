namespace APISitemaUnivalle.Models.Request.Servicios
{
    public class servicio_add_request
    {
        public string Nombre { get; set; } = null!;
        public int ModuloId { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
