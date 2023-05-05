using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IServicioBase<clsProducto, int> db;
        private readonly IServicioBase<clsTipoUnidad, int> dbTipoUnidad;
        private readonly IServicioBase<clsCategoria, int> dbCategoria;

        public ProductoController(IServicioBase<clsProducto, int> _db, IServicioBase<clsTipoUnidad, int> _dbTipoUnidad, IServicioBase<clsCategoria, int> _dbCategoria)
        {
            db = _db;
            dbTipoUnidad = _dbTipoUnidad;
            dbCategoria = _dbCategoria;
        }

        public async Task<IActionResult> vProductoIni()
        {
            List<clsProducto> objeto = await db.ObtenerTodo();

            foreach (clsProducto item in objeto)
            {
                item.TipoUnidad = await dbTipoUnidad.ObtenerPorID(item.IdTipoUnidad);
            }

            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vProductoDet(int _IdProducto)
        {

            clsProducto modelo_producto = new clsProducto();

            ViewBag.Accion = "Nuevo";

            var objeto = new SelectList(await dbTipoUnidad.ObtenerTodo(), "IdTipoUnidad", "strTipoUnidad");
            ViewData["TipoUnidad"] = objeto;

            var objeto2 = new SelectList(await dbCategoria.ObtenerTodo(), "IdCategoria", "strCategoria");
            ViewData["Categoria"] = objeto2;

            if (_IdProducto != 0)
            {

                ViewBag.Accion = "Actualizar";
                modelo_producto = await db.ObtenerPorID(_IdProducto);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsProducto _objeto)
        {

            bool respuesta;

            _objeto.bnReferencia = new byte[0x20];

            if (_objeto.IdProducto == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vProductoIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdProducto)
        {

            var respuesta = await db.Eliminar(_IdProducto);

            if (respuesta)
                return RedirectToAction("vProductoIni");
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
