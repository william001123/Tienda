
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsVentaTemp
    {
        public int IdVentaDetalle { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public double numCantidad { get; set; }

        public double numPrecio { get; set; }

        public double numTotal { get; set; }

        public clsProducto Producto { get; set; }

        public clsVentaTemp() {

            IdVentaDetalle = 0; 
            IdVenta = 0; 
            IdProducto = 0;
            numCantidad = 0; 
            numPrecio = 0; 
            numTotal = 0; 

            Producto = new clsProducto();
        }   
    }
}