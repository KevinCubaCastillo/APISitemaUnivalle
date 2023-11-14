namespace APISitemaUnivalle.Models.Request.Publicacion
{
    public class Publicacion_edit_Request
    {
        public string Archivo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public List<descripcionEditPub> descripcionPublicacion { get; set; }
        public Publicacion_edit_Request()
        {
            descripcionPublicacion = new List<descripcionEditPub>();
        }

    }
    public class descripcionEditPub
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = null!;

    }
}
