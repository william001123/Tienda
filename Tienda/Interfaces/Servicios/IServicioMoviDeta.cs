
namespace Tienda.Interfaces.Servicios
{
    public interface IServicioMoviDeta<TEntidad, TEntidadID, TEntidadTip>
        : IListar<TEntidad, TEntidadID>
    {
        Task<TEntidad> Insertar(TEntidad entidad, TEntidadID TipoMovimiento);
        Task<TEntidadTip> ObtenerPorIDEnc(TEntidadID entidadID);
    }
}
