using APISitemaUnivalle.Models.Request.Referencias;
using APISitemaUnivalle.Models.Request.Requisitos;
using APISitemaUnivalle.Models.Request.Ubicacion;

namespace APISitemaUnivalle.Servicios
{
    public interface IService
    {
        public void addUbicacion(ubicacion_add_request oModel);
        public void addReferencia(Referencias_add_Request oModel);
        public void addRequisito(requisito_add_request oModel);

    }
}
