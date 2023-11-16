namespace APISitemaUnivalle.Models.Request.Usuario
{
    public class permisos_add_request
    {
        public List<modulos> listModulo { get; set; }
        public permisos_add_request()
        {
            listModulo = new List<modulos>();
        }
    }
}
