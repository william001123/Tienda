
namespace Tienda.Interfaces.Servicios
{
    public interface IServicioObteIDEnc<TEntidad, TEntidadID, TEntidadTip>
        : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>
    {
        Task<TEntidadTip> ObtenerPorIDEnc(TEntidadID entidadID);
    }
}
