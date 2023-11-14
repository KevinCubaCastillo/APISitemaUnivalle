namespace APISitemaUnivalle.Models.Request.Horarios
{
    public class horario_add_request
    {
        public string HoraInicio { get; set; } = null!;
        public string HoraFin { get; set; } = null!;
        public int? IdServicio { get; set; }
        public int? IdModulo { get; set; }
        public List<atencionAddReq> listAtencion { get; set; }
        public horario_add_request()
        {
            listAtencion = new List<atencionAddReq>();
        }
    }
    public class atencionAddReq
    {
        public int? IdDia { get; set; }
    }
}
