namespace APISitemaUnivalle.Models.Request.Publicacion
{
    public class Publicacion_add_Request
    {
       // public int Id { get; set; }
        public string Archivo { get; set; } = null!;
        public int ServiciosId { get; set; }
        public string Titulo { get; set; } = null!;
        public int id_modulo { get; set; }
        public bool Estado { get; set; }
        public List<descripcionPub> descripcionPublicacion { get; set; }
        public Publicacion_add_Request()
        {
            descripcionPublicacion = new List<descripcionPub>();
        }
        
    }
    public class descripcionPub
    {
        public string Contenido { get; set; } = null!;

    }
}
