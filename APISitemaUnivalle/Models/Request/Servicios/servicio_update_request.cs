namespace APISitemaUnivalle.Models.Request.Servicios
{
    public class servicio_update_request
    {
        public string Nombre { get; set; } = null!;
        public string? ImagenUrl { get; set; }
        public int? IdCategoria { get; set; }
    }
}
