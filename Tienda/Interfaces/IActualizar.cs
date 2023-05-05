using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Interfaces
{
    public interface IActualizar<TEntidad>
    {
        Task<bool> Actualizar(TEntidad entidad);

    }
}
