namespace APISitemaUnivalle.Models.Request.Usuario
{
    public class usuario_modulo_add_request
    {
        public string CiUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int CargoId { get; set; }
        public List<modulos> listModulo { get; set; }
        public usuario_modulo_add_request()
        {
            listModulo = new List<modulos>();
        }
    }
    public class modulos
    {
        public int id_modulo { get; set; }
    }
}
