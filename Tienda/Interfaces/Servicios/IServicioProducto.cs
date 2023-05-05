
namespace Tienda.Interfaces.Servicios
{
    public interface IServicioProducto<TEntidad, TEntidadID, TEntidadID2>
        : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>
    {
        void ActualizarCant(TEntidadID entidadID, TEntidadID2 entidadCantidad);
    }
}
