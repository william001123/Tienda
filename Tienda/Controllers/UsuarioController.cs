using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IServicioObteIDEnc<clsUsuario, int, clsUsuario> db;

        public UsuarioController(IServicioObteIDEnc<clsUsuario, int, clsUsuario> _db)
        {
            db = _db;
        }

        public async Task<IActionResult> vUsuarioIni()
        {
            List<clsUsuario> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vUsuarioDet(int _UserId)
        {

            clsUsuario modelo_producto = new clsUsuario();

            //ViewBag.Accion = "Nuevo";

            if (_UserId != 0)
            {

                //ViewBag.Accion = "Actualizar";
                modelo_producto = await db.ObtenerPorID(_UserId);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsUsuario _objeto)
        {

            bool respuesta;

            if (_objeto.UserId == 0)
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

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _UserId)
        {

            var respuesta = await db.Eliminar(_UserId);

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
