using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class OrdenDetalleController : Controller
    {       
        private readonly IServicioObteIDEnc<clsOrdenDetalle, int, List<clsOrdenDetalle>> dbOrdenDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;

        public OrdenDetalleController(IServicioObteIDEnc<clsOrdenDetalle, int, List<clsOrdenDetalle>> _dbOrdenDetalle, IServicioBase<clsProducto, int> _dbProducto)
        {           
            dbOrdenDetalle = _dbOrdenDetalle;
            dbProducto = _dbProducto;
        }

        //public async Task<IActionResult> vOrdenDetalleIni()
        //{
        //    List<clsOrdenDetalle> objeto = await dbOrdenDetalle.ObtenerTodo();
        //    return View(objeto);
        //}

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vOrdenDetalleDet(int _IdOrden, int _IdOrdenDetalle)
        {

            clsOrdenDetalle modelo = new clsOrdenDetalle();
            modelo.IdOrden = _IdOrden;

            ViewBag.Accion = "Nuevo";

            var objeto = new SelectList(await dbProducto.ObtenerTodo(), "IdProducto", "strNombre");
            ViewData["Producto"] = objeto;

            if (_IdOrdenDetalle != 0)
            {
                ViewBag.Accion = "Actualizar";
                modelo = await dbOrdenDetalle.ObtenerPorID(_IdOrdenDetalle);                
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsOrdenDetalle _objeto)
        {

            bool respuesta;

            if (_objeto.IdOrdenDetalle == 0)
            {
                respuesta = await dbOrdenDetalle.Insertar(_objeto);
            }
            else
            {
                respuesta = await dbOrdenDetalle.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vOrdenDet", "Orden", new { _IdOrden = _objeto.IdOrden });
            else
                return NoContent();

        }

        //[HttpPost]
        //public async Task<IActionResult> GuardarCambiosOrdenDetalle(clsOrden _objeto)
        //{

        //    bool respuesta=false;

        //    if (_objeto.IdOrden == 0)
        //    {
        //        foreach (clsOrdenDetalle item in _objeto.OrdenDetalles)
        //        {
        //            respuesta = await dbOrdenDetalle.Insertar(item);
        //        }
                
        //    }
        //    else
        //    {
        //        foreach (clsOrdenDetalle item in _objeto.OrdenDetalles)
        //        {
        //            respuesta = await dbOrdenDetalle.Actualizar(item);
        //        }                
        //    }

        //    if (respuesta)
        //        return RedirectToAction("vOrdenDet", _objeto.IdOrden);
        //    else
        //        return NoContent();

        //}

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdOrden, int _IdOrdenDetalle)
        {

            var respuesta = await dbOrdenDetalle.Eliminar(_IdOrdenDetalle);

            if (respuesta)
                return RedirectToAction("vOrdenDet", "Orden", new { _IdOrden = _IdOrden });
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
