using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Interfaces
{
    public interface IListar<TEntidad, TEntidadID>
    {
        Task<List<TEntidad>> ObtenerTodo();

        Task<TEntidad> ObtenerPorID(TEntidadID entidadID);
    }
}
