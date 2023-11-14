namespace APISitemaUnivalle.Models.Request.Referencias
{
    public class Referencias_add_Request
    {

        public string Nombre { get; set; } = null!;
        public string Numerocel { get; set; } = null!;
        public int? ServiciosId { get; set; }
        public int? id_modulo { get; set; }
        public bool Estado { get; set; }
    }
}
