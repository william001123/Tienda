
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsMovimiento
    {

        public int IdMovimiento { get; set; }

        public int IdTipoMovimiento { get; set; }

        public int IdTipoOrigen { get; set; }

        public int IdOrigen { get; set; }

        public int UserId { get; set; }

        public List<clsMovimientoDetalle> MovimientoDetalles { get; set; }


        public clsMovimiento() 
        {
            IdMovimiento = 0;
            IdTipoMovimiento = 0;
            IdTipoOrigen = 0;
            IdOrigen = 0;
            UserId = 0;

            MovimientoDetalles = new List<clsMovimientoDetalle>();
        }

    }
}