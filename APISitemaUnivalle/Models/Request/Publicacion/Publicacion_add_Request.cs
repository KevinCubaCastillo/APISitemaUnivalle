namespace APISitemaUnivalle.Models.Request.Publicacion
{
    public class Publicacion_add_Request
    {
       // public int Id { get; set; }
        public string Archivo { get; set; } = null!;
        public int ServiciosId { get; set; }
        public string Titulo { get; set; } = null!;
        public bool Estado { get; set; }
        
    }
}
