using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Interfaces.Servicios
{
    public interface IServicioVenta<TEntidad, TEntidadID>
        : IActualizar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>
    {
        Task<TEntidad> Insertar(TEntidad entidad);
    }
}
