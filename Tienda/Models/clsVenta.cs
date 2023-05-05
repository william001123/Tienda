
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsVenta
    {

        public int IdVenta { get; set; }

        public string strVenta { get; set; }

        public DateTime dtFecha { get; set; }

        public double numTotal { get; set; }

        public string strEstado { get; set; }

        public List<clsVentaDetalle> VentaDetalles { get; set; }

        public clsVenta()
        {
            IdVenta = 0;
            strVenta = string.Empty;
            dtFecha = DateTime.Now;
            numTotal = 0;
            strEstado = "Nuevo";

            VentaDetalles = new List<clsVentaDetalle>();
        }
    }
}