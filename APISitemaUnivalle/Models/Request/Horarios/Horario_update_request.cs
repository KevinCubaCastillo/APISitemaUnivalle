namespace APISitemaUnivalle.Models.Request.Horarios
{
    public class Horario_update_request
    {
        public string HoraInicio { get; set; } = null!;
        public string HoraFin { get; set; } = null!;
        public int? IdServicio { get; set; }
        public int? IdModulo { get; set; }
        public List<atencionAddReq> listAtencion { get; set; }
        public Horario_update_request()
        {
            listAtencion = new List<atencionAddReq>();
        }
    }
}
