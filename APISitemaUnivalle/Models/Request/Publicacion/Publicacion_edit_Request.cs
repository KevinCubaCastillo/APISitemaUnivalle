namespace APISitemaUnivalle.Models.Request.Publicacion
{
    public class Publicacion_edit_Request
    {
        //public int Id { get; set; }
        public string Archivo { get; set; } = null!;
        public string? Descripcion1 { get; set; }
        public int ServiciosId { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descripcion2 { get; set; }
        //public bool Estado { get; set; }
    }
}
