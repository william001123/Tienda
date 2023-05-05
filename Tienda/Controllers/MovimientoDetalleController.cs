using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class MovimientoDetalleController : Controller
    {       
        private readonly IServicioObteIDEnc<clsMovimientoDetalle, int, List<clsMovimientoDetalle>> dbMovimientoDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;

        public MovimientoDetalleController(IServicioObteIDEnc<clsMovimientoDetalle, int, List<clsMovimientoDetalle>> _dbMovimientoDetalle, IServicioBase<clsProducto, int> _dbProducto)
        {
            dbMovimientoDetalle = _dbMovimientoDetalle;
            dbProducto = _dbProducto;
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vMovimientoDetalleDet(int _IdMovimiento, int _IdMovimientoDetalle)
        {

            clsMovimientoDetalle modelo = new clsMovimientoDetalle();
            modelo.IdMovimiento = _IdMovimiento;

            ViewBag.Accion = "Nuevo";

            var objeto = new SelectList(await dbProducto.ObtenerTodo(), "IdProducto", "strNombre");
            ViewData["Producto"] = objeto;

            if (_IdMovimientoDetalle != 0)
            {
                ViewBag.Accion = "Actualizar";
                modelo = await dbMovimientoDetalle.ObtenerPorID(_IdMovimientoDetalle);                
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsMovimientoDetalle _objeto)
        {

            bool respuesta;

            if (_objeto.IdMovimientoDetalle == 0)
            {
                respuesta = await dbMovimientoDetalle.Insertar(_objeto);
            }
            else
            {
                respuesta = await dbMovimientoDetalle.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vMovimientoDet", "Movimiento", new { _IdMovimiento = _objeto.IdMovimiento });
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdMovimiento, int _IdMovimientoDetalle)
        {

            var respuesta = await dbMovimientoDetalle.Eliminar(_IdMovimientoDetalle);

            if (respuesta)
                return RedirectToAction("vMovimientoDet", "Movimiento", new { _IdMovimiento = _IdMovimiento });
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
