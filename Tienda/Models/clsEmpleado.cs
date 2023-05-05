
namespace Tienda.Models
{
    public class clsEmpleado
    {

        public int IdEmpleado { get; set; }

        public string strDocumento { get; set; }

        public string strNombre { get; set; }

        public clsUsuario Usuario { get; set; }

    }
}