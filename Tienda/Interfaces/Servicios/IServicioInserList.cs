
namespace Tienda.Interfaces.Servicios
{
    public interface IServicioInserList<TEntidad, TEntidadID>
        : IInsertar<TEntidad>, IListar<TEntidad, TEntidadID>
    {
    }
}
