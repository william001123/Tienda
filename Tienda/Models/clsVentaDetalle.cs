
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsVentaDetalle
    {
        public int IdVentaDetalle { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public double numCantidad { get; set; }

        public double numPrecio { get; set; }

        public clsProducto Producto { get; set; }
    }
}