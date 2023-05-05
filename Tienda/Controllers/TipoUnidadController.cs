using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class TipoUnidadController : Controller
    {

        private readonly IServicioBase<clsTipoUnidad, int> db;

        public TipoUnidadController(IServicioBase<clsTipoUnidad, int> _db)
        {
            db = _db;
        }

        public async Task<IActionResult> vTipoUnidadIni()
        {
            List<clsTipoUnidad> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vTipoUnidadDet(int _IdTipoUnidad)
        {

            clsTipoUnidad modelo_producto = new clsTipoUnidad();

            ViewBag.Accion = "Nuevo";

            if (_IdTipoUnidad != 0)
            {

                ViewBag.Accion = "Actualizar";
                modelo_producto = await db.ObtenerPorID(_IdTipoUnidad);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsTipoUnidad _objeto)
        {

            bool respuesta;

            if (_objeto.IdTipoUnidad == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vTipoUnidadIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdTipoUnidad)
        {

            var respuesta = await db.Eliminar(_IdTipoUnidad);

            if (respuesta)
                return RedirectToAction("vTipoUnidadIni");
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
