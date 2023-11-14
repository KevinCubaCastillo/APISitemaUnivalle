namespace APISitemaUnivalle.Models.Request.Usuario
{
    public class usuario_add_request
    {
        public string CiUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int CargoId { get; set; }
    }
}
