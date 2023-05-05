
namespace Tienda.Interfaces.Servicios
{
    public interface IServicioActuaEstado<TEntidad, TEntidadID, TEntidadID2>
        : IServicioInserList<TEntidad, TEntidadID>, IEliminar<TEntidadID>
    {
        Task<bool> ActualizarEstado(TEntidadID entidadID, TEntidadID2 entidadEstado);
    }
}
