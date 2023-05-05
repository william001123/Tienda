
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsOrden
    {

        public int IdOrden { get; set; }

        public int IdProveedor { get; set; }

        public string strOrden { get; set; }

        public DateTime dtFechaOrden { get; set; }

        public DateTime dtFechaEntrega { get; set; }

        public double numTotal { get; set; }

        public int UserId { get; set; }

        public string strEstado { get; set; }

        public List<clsOrdenDetalle> OrdenDetalles { get; set; }

        public clsProveedor Proveedor { get; set; }

        public List<clsRecepcion> Recepcions { get; set; }

        public clsOrden()
        {
            IdOrden = 0;
            IdProveedor = 0;
            strOrden = string.Empty;
            dtFechaOrden = DateTime.Now;
            dtFechaEntrega = DateTime.Now;
            numTotal = 0.00;
            UserId = 0;
            strEstado = "Nuevo";

            OrdenDetalles = new List<clsOrdenDetalle>();
            Proveedor = new clsProveedor();
            Recepcions = new List<clsRecepcion>();
        }

    }
}