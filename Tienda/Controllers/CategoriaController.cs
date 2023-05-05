using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class CategoriaController : Controller
    {

        private readonly IServicioBase<clsCategoria, int> db;

        public CategoriaController(IServicioBase<clsCategoria, int> _db)
        {
            db = _db;
        }

        public async Task<IActionResult> vCategoriaIni()
        {
            List<clsCategoria> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vCategoriaDet(int _IdCategoria)
        {

            clsCategoria modelo_producto = new clsCategoria();

            ViewBag.Accion = "Nuevo";

            if (_IdCategoria != 0)
            {

                ViewBag.Accion = "Actualizar";
                modelo_producto = await db.ObtenerPorID(_IdCategoria);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsCategoria _objeto)
        {

            bool respuesta;

            if (_objeto.IdCategoria == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vCategoriaIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdCategoria)
        {

            var respuesta = await db.Eliminar(_IdCategoria);

            if (respuesta)
                return RedirectToAction("vCategoriaIni");
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
