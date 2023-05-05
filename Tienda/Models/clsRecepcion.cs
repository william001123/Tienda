
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsRecepcion
    {

        public int IdRecepcion { get; set; }

        public string strRecepcion { get; set; }
        public string strFactura { get; set; }

        public string strObservaciones { get; set; }

        public int IdOrden { get; set; }

        public string strEstado { get; set; }

        public List<clsRecepcionDetalle> RecepcionDetalles { get; set; }

        public clsOrden Orden { get; set; }

        public clsRecepcion()
        {
            IdRecepcion = 0;
            strRecepcion = string.Empty;
            strObservaciones = string.Empty;
            IdOrden = 0;
            strEstado = "Nuevo";

            RecepcionDetalles = new List<clsRecepcionDetalle>();
            Orden = new clsOrden();
        }

    }
}