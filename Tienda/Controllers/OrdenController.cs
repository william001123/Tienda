using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class OrdenController : Controller
    {

        private readonly IServicioBase<clsOrden, int> db;
        private readonly IServicioObteIDEnc<clsOrdenDetalle, int, List<clsOrdenDetalle>> dbOrdenDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;
        private readonly IServicioBase<clsProveedor, int> dbProveedor;

        public OrdenController(IServicioBase<clsOrden, int> _db, IServicioObteIDEnc<clsOrdenDetalle, int, List<clsOrdenDetalle>> _dbOrdenDetalle, IServicioBase<clsProducto, int> _dbProducto, IServicioBase<clsProveedor, int> _dbProveedor)
        {
            db = _db;
            dbOrdenDetalle = _dbOrdenDetalle;
            dbProducto = _dbProducto;
            dbProveedor = _dbProveedor;
        }

        public async Task<IActionResult> vOrdenIni()
        {
            List<clsOrden> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vOrdenDet(int _IdOrden)
        {

            clsOrden modelo = new clsOrden();

            ViewBag.Accion = "Nuevo";
            ViewBag.boton = "Guardar Cambios";

            var fromDatabaseEF = new SelectList(await dbProveedor.ObtenerTodo(), "IdProveedor", "strNombre");
            ViewData["Proveedores"] = fromDatabaseEF;


            if (_IdOrden != 0)
            {

                ViewBag.Accion = "Actualizar";
                ViewBag.boton = "Procesar Recepción";
                modelo = await db.ObtenerPorID(_IdOrden);
                modelo.OrdenDetalles = await dbOrdenDetalle.ObtenerPorIDEnc(_IdOrden);
                foreach (var item in modelo.OrdenDetalles)
                {
                    item.Producto = await dbProducto.ObtenerPorID(item.IdProducto);
                }               
            }

            return View(modelo);
        }

        //public async Task<IActionResult> vOrdenDetalleIni()
        //{
        //    List<clsOrdenDetalle> objeto = await dbOrdenDetalle.ObtenerTodo();
        //    return View(objeto);
        //}

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        //public async Task<IActionResult> vOrdenDetalleDet(int _IdOrden)
        //{

        //    clsOrdenDetalle modelo = new clsOrdenDetalle();

        //    ViewBag.Accion = "Nuevo";
        //    ViewBag.boton = "Guardar Cambios";

        //    if (_IdOrden != 0)
        //    {
        //        ViewBag.boton = "Procesar Recepción";
        //        ViewBag.Accion = "Actualizar";
        //        modelo = await dbOrdenDetalle.ObtenerPorID(_IdOrden);                
        //    }

        //    return View(modelo);
        //}

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsOrden _objeto)
        {

            bool respuesta;            

            if (_objeto.IdOrden == 0)
            {
                respuesta = await db.Insertar(_objeto);
            }
            else
            {
                _objeto.strEstado = "Procesada";
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vOrdenIni");
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
        //        return RedirectToAction("vOrdenIni");
        //    else
        //        return NoContent();

        //}

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdOrden)
        {

            var respuesta = await db.Eliminar(_IdOrden);

            if (respuesta)
                return RedirectToAction("vOrdenIni");
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
