using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class VentaDetalleController : Controller
    {       
        private readonly IServicioObteIDEnc<clsVentaDetalle, int, List<clsVentaDetalle>> dbVentaDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;

        public VentaDetalleController(IServicioObteIDEnc<clsVentaDetalle, int, List<clsVentaDetalle>> _dbVentaDetalle, IServicioBase<clsProducto, int> _dbProducto)
        {           
            dbVentaDetalle = _dbVentaDetalle;
            dbProducto = _dbProducto;
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vVentaDetalleDet(int _IdVenta, int _IdVentaDetalle)
        {

            clsVentaDetalle modelo = new clsVentaDetalle();

            modelo.IdVenta = _IdVenta;

            ViewBag.Accion = "Nuevo";

            var objeto = new SelectList(await dbProducto.ObtenerTodo(), "IdProducto", "strNombre");
            ViewData["Producto"] = objeto;

            if (_IdVentaDetalle != 0)
            {
                ViewBag.Accion = "Actualizar";
                modelo = await dbVentaDetalle.ObtenerPorID(_IdVentaDetalle);                
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsVentaDetalle _objeto)
        {

            bool respuesta;

            if (_objeto.IdVentaDetalle == 0)
            {
                respuesta = await dbVentaDetalle.Insertar(_objeto);
            }
            else
            {
                respuesta = await dbVentaDetalle.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vVentaDet", "Venta", new { _IdVenta = _objeto.IdVenta });
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdVenta, int _IdVentaDetalle)
        {

            var respuesta = await dbVentaDetalle.Eliminar(_IdVentaDetalle);

            if (respuesta)
                return RedirectToAction("vVentaDet", "Venta", new { _IdVenta = _IdVenta });
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
