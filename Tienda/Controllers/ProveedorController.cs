using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ProveedorController : Controller
    {

        private readonly IServicioBase<clsProveedor, int> db;

        public ProveedorController(IServicioBase<clsProveedor, int> _db)
        {
            db = _db;
        }

        public async Task<IActionResult> vProveedorIni()
        {
            List<clsProveedor> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vProveedorDet(int _IdProveedor)
        {

            clsProveedor modelo_producto = new clsProveedor();

            ViewBag.Accion = "Nuevo";

            if (_IdProveedor != 0)
            {

                ViewBag.Accion = "Actualizar";
                modelo_producto = await db.ObtenerPorID(_IdProveedor);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsProveedor _objeto)
        {

            bool respuesta;

            if (_objeto.IdProveedor == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vProveedorIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdProveedor)
        {

            var respuesta = await db.Eliminar(_IdProveedor);

            if (respuesta)
                return RedirectToAction("vProveedorIni");
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
