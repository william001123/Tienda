using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;

namespace Tienda.Controllers
{
    public class RecepcionDetalleController : Controller
    {       
        private readonly IServicioObteIDEnc<clsRecepcionDetalle, int, List<clsRecepcionDetalle>> dbRecepcionDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;

        public RecepcionDetalleController(IServicioObteIDEnc<clsRecepcionDetalle, int, List<clsRecepcionDetalle>> _dbRecepcionDetalle, IServicioBase<clsProducto, int> _dbProducto)
        {           
            dbRecepcionDetalle = _dbRecepcionDetalle;
            dbProducto = _dbProducto;
        }

        //public async Task<IActionResult> vRecepcionDetalleIni()
        //{
        //    List<clsRecepcionDetalle> objeto = await dbRecepcionDetalle.ObtenerTodo();
        //    return View(objeto);
        //}

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vRecepcionDetalleDet(int _IdRecepcion, int _IdRecepcionDetalle)
        {

            clsRecepcionDetalle modelo = new clsRecepcionDetalle();

            modelo.IdRecepcion = _IdRecepcion;

            ViewBag.Accion = "Nuevo";

            var objeto = new SelectList(await dbProducto.ObtenerTodo(), "IdProducto", "strNombre");
            ViewData["Producto"] = objeto;

            if (_IdRecepcionDetalle != 0)
            {
                ViewBag.Accion = "Actualizar";
                modelo = await dbRecepcionDetalle.ObtenerPorID(_IdRecepcionDetalle);                
            }

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsRecepcionDetalle _objeto)
        {

            bool respuesta;

            if (_objeto.IdRecepcionDetalle == 0)
            {
                respuesta = await dbRecepcionDetalle.Insertar(_objeto);
            }
            else
            {
                respuesta = await dbRecepcionDetalle.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vRecepcionDet", "Recepcion", new { _IdRecepcion = _objeto.IdRecepcion });
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdRecepcion, int _IdRecepcionDetalle)
        {

            var respuesta = await dbRecepcionDetalle.Eliminar(_IdRecepcionDetalle);

            if (respuesta)
                return RedirectToAction("vRecepcionDet", "Recepcion", new { _IdRecepcion = _IdRecepcion });
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
