namespace APISitemaUnivalle.Models.Request.Requisitos
{
    public class requisito_update_request
    {
        public string Descripcion { get; set; } = null!;

        public List<pasosUpdateRequisitos> pasos { get; set; }
        public requisito_update_request()
        {
            this.pasos = new List<pasosUpdateRequisitos>();
        }
    }
    public class pasosUpdateRequisitos
    {
        public int id { get; set; }
        public string? Nombre { get; set; }
    }
}
