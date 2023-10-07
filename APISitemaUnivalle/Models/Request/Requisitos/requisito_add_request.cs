namespace APISitemaUnivalle.Models.Request.Requisitos
{
    public class requisito_add_request
    {
        public string Descripcion { get; set; } = null!;
        public int ServiciosId { get; set; }
        public List<pasosRequisitos> pasos { get; set; }
        public requisito_add_request()
        {
            this.pasos = new List<pasosRequisitos>();
        }
    }
    public class pasosRequisitos
    {
        public string Nombre { get; set; }
    }
}
