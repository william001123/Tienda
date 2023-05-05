
namespace Tienda.Models
{
    public class clsRecepcionDetalle
    {

        public int IdRecepcionDetalle { get; set; }

        public int IdRecepcion { get; set; }

        public int IdProducto { get; set; }

        public double numCantidad { get; set; }

        public double numPrecio { get; set; }

        public clsProducto Producto { get; set; }
    }
}