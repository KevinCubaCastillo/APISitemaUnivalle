namespace APISitemaUnivalle.Models.Request.Usuario
{
    public class usuario_add_request
    {
        public string? Clave { get; set; } = null!;
        public bool Estado { get; set; }
        public int PersonalId { get; set; }
    }
}
