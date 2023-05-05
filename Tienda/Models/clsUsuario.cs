
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tienda.Models
{
    public class clsUsuario
    {
        public int UserId { get; set; }

        public string strUserName { get; set; }

        public string strContraseña { get; set; }

        public int IdEmpleado { get; set; }

        //public clsEmpleado Empleado { get; set; }
    }
}