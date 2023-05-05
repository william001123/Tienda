using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tienda.Interfaces
{
    public interface IEliminar<TEntidadID>
    {
        Task<bool> Eliminar(TEntidadID entidadID);   
    }
}
