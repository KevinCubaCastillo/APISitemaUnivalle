namespace APISitemaUnivalle.Models.Request.Ubicacion
{
    public class ubicacion_add_request
    {
        public string? Descripcion { get; set; } = null!;
        public string? Imagen { get; set; }
        public string? Video { get; set; }
        public int? ServiciosId { get; set; }
        public bool? Estado { get; set; }
    }
}
