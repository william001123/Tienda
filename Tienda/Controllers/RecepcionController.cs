using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class RecepcionController : Controller
    {

        private readonly IServicioBase<clsRecepcion, int> db;
        private readonly IServicioObteIDEnc<clsRecepcionDetalle, int, List<clsRecepcionDetalle>> dbRecepcionDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;
        private readonly IServicioBase<clsOrden, int> dbOrden;

        public RecepcionController(IServicioBase<clsRecepcion, int> _db, 
                                   IServicioObteIDEnc<clsRecepcionDetalle, int, List<clsRecepcionDetalle>> _dbRecepcionDetalle, 
                                   IServicioBase<clsProducto, int> _dbProducto,
                                   IServicioBase<clsOrden, int> _dbOrden)
        {
            db = _db;
            dbRecepcionDetalle = _dbRecepcionDetalle;
            dbProducto = _dbProducto;
            dbOrden = _dbOrden;
        }

        public async Task<IActionResult> vRecepcionIni()
        {
            List<clsRecepcion> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vRecepcionDet(int _IdRecepcion)
        {

            clsRecepcion modelo = new clsRecepcion();

            ViewBag.Accion = "Nuevo";
            ViewBag.boton = "Guardar Cambios";

            var fromDatabaseEF = new SelectList(await dbOrden.ObtenerTodo(), "IdOrden", "strOrden");
            ViewData["Ordenes"] = fromDatabaseEF;


            if (_IdRecepcion != 0)
            {

                ViewBag.Accion = "Actualizar";
                ViewBag.boton = "Procesar Recepción";

                modelo = await db.ObtenerPorID(_IdRecepcion);
                modelo.RecepcionDetalles = await dbRecepcionDetalle.ObtenerPorIDEnc(_IdRecepcion);
                foreach (var item in modelo.RecepcionDetalles)
                {
                    item.Producto = await dbProducto.ObtenerPorID(item.IdProducto);
                }               
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsRecepcion _objeto)
        {

            bool respuesta;

            if (_objeto.IdRecepcion == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                _objeto.strEstado = "Procesada";
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vRecepcionIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdRecepcion)
        {

            var respuesta = await db.Eliminar(_IdRecepcion);

            if (respuesta)
                return RedirectToAction("vRecepcionIni");
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
