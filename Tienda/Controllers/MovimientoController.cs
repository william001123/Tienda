using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class MovimientoController : Controller
    {

        private readonly IServicioBase<clsMovimiento, int> db;
        private readonly IServicioObteIDEnc<clsMovimientoDetalle, int, List<clsMovimientoDetalle>> dbMovimientoDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;     

        public MovimientoController(IServicioBase<clsMovimiento, int> _db, IServicioObteIDEnc<clsMovimientoDetalle, int, List<clsMovimientoDetalle>> _dbMovimientoDetalle, IServicioBase<clsProducto, int> _dbProducto)
        {
            db = _db;
            dbMovimientoDetalle = _dbMovimientoDetalle;
            dbProducto = _dbProducto;            
        }

        public async Task<IActionResult> vMovimientoIni()
        {
            List<clsMovimiento> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vMovimientoDet(int _IdMovimiento)
        {

            clsMovimiento modelo = new clsMovimiento();

            ViewBag.Accion = "Nuevo";

            if (_IdMovimiento != 0)
            {

                ViewBag.Accion = "Actualizar";
                modelo = await db.ObtenerPorID(_IdMovimiento);
                modelo.MovimientoDetalles = await dbMovimientoDetalle.ObtenerPorIDEnc(_IdMovimiento);
                foreach (var item in modelo.MovimientoDetalles)
                {
                    item.Producto = await dbProducto.ObtenerPorID(item.IdProducto);
                }               
            }

            return View(modelo);
        }

        public async Task<IActionResult> vMovimientoDetalleIni()
        {
            List<clsMovimientoDetalle> objeto = await dbMovimientoDetalle.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vMovimientoDetalleDet(int _IdMovimiento)
        {

            clsMovimientoDetalle modelo = new clsMovimientoDetalle();

            ViewBag.Accion = "Nuevo";

            if (_IdMovimiento != 0)
            {
                ViewBag.Accion = "Actualizar";
                modelo = await dbMovimientoDetalle.ObtenerPorID(_IdMovimiento);                
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsMovimiento _objeto)
        {

            bool respuesta;            

            if (_objeto.IdMovimiento == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vMovimientoIni");
            else
                return NoContent();

        }

        //[HttpPost]
        //public async Task<IActionResult> GuardarCambiosMovimientoDetalle(clsMovimiento _objeto)
        //{

        //    bool respuesta=false;

        //    if (_objeto.IdMovimiento == 0)
        //    {
        //        foreach (clsMovimientoDetalle item in _objeto.MovimientoDetalles)
        //        {
        //            respuesta = await dbMovimientoDetalle.Insertar(item);
        //        }
                
        //    }
        //    else
        //    {
        //        foreach (clsMovimientoDetalle item in _objeto.MovimientoDetalles)
        //        {
        //            respuesta = await dbMovimientoDetalle.Actualizar(item);
        //        }                
        //    }

        //    if (respuesta)
        //        return RedirectToAction("vMovimientoIni");
        //    else
        //        return NoContent();

        //}

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdMovimiento)
        {

            var respuesta = await db.Eliminar(_IdMovimiento);

            if (respuesta)
                return RedirectToAction("vMovimientoIni");
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
