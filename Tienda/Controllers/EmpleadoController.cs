using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class EmpleadoController : Controller
    {

        private readonly IServicioBase<clsEmpleado, int> db;
        private readonly IServicioObteIDEnc<clsUsuario, int, clsUsuario> dbUsuario;

        public EmpleadoController(IServicioBase<clsEmpleado, int> _db, IServicioObteIDEnc<clsUsuario, int, clsUsuario> _dbUsuario)
        {
            db = _db;
            dbUsuario = _dbUsuario;
        }

        public async Task<IActionResult> vEmpleadoIni()
        {
            List<clsEmpleado> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vEmpleadoDet(int _IdEmpleado)
        {

            clsEmpleado modelo_producto = new clsEmpleado();

            ViewBag.Accion = "Nuevo";

            if (_IdEmpleado != 0)
            {

                ViewBag.Accion = "Actualizar";
                modelo_producto = await db.ObtenerPorID(_IdEmpleado);
                modelo_producto.Usuario = await dbUsuario.ObtenerPorIDEnc(_IdEmpleado);
                
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsEmpleado _objeto)
        {

            bool respuesta;

            if (_objeto.IdEmpleado == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vEmpleadoIni");
            else
                return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambiosUsuario(clsEmpleado _objeto)
        {

            bool respuesta;

            _objeto.Usuario.IdEmpleado = _objeto.IdEmpleado;

            if (_objeto.Usuario.UserId == 0)
            {
                respuesta = await dbUsuario.Insertar(_objeto.Usuario);
            }
            else
            {
                respuesta = await dbUsuario.Actualizar(_objeto.Usuario);
            }

            if (respuesta)
                return RedirectToAction("vEmpleadoIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdEmpleado)
        {

            var respuesta = await db.Eliminar(_IdEmpleado);

            if (respuesta)
                return RedirectToAction("vEmpleadoIni");
            else
                return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
