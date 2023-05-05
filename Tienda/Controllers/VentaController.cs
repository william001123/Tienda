using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using System.Diagnostics;
using Tienda.Interfaces.Servicios;
using Tienda.Models;
using Newtonsoft.Json;

namespace Tienda.Controllers
{
    public class VentaController : Controller
    {

        private readonly IServicioVenta<clsVenta, int> db;
        private readonly IServicioObteIDEnc<clsVentaDetalle, int, List<clsVentaDetalle>> dbVentaDetalle;
        private readonly IServicioBase<clsProducto, int> dbProducto;
        private readonly IServicioMovimiento<clsMovimiento, int> dbMovimiento;
        private readonly IServicioMoviDeta<clsMovimientoDetalle, int, List<clsMovimientoDetalle>> dbMovimientoDetalle;

        public VentaController(IServicioVenta<clsVenta, int> _db, 
                                   IServicioObteIDEnc<clsVentaDetalle, int, List<clsVentaDetalle>> _dbVentaDetalle, 
                                   IServicioBase<clsProducto, int> _dbProducto,
                                   IServicioMovimiento<clsMovimiento, int> _dbMovimiento,
                                   IServicioMoviDeta<clsMovimientoDetalle, int, List<clsMovimientoDetalle>> _dbMovimientoDetalle)
        {
            db = _db;
            dbVentaDetalle = _dbVentaDetalle;
            dbProducto = _dbProducto;
            dbMovimiento = _dbMovimiento;
            dbMovimientoDetalle = _dbMovimientoDetalle;
        }

        public async Task<IActionResult> vVentaIni()
        {
            List<clsVenta> objeto = await db.ObtenerTodo();
            return View(objeto);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR
        public async Task<IActionResult> vVentaDet(int _IdVenta)
        {

            clsVenta modelo = new clsVenta();

            ViewBag.Accion = "Nuevo";

            if (_IdVenta != 0)
            {

                ViewBag.Accion = "Ver";
                modelo = await db.ObtenerPorID(_IdVenta);
                modelo.VentaDetalles = await dbVentaDetalle.ObtenerPorIDEnc(_IdVenta);
                foreach (var item in modelo.VentaDetalles)
                {
                    item.Producto = await dbProducto.ObtenerPorID(item.IdProducto);
                }
            }

            return View(modelo);
        }

        public async Task<IActionResult> vVentaTemp(int vista)
        {

            List<clsVentaTemp> modelo = new List<clsVentaTemp>();

            ViewBag.Accion = "Nuevo";

            var objeto = new SelectList(await dbProducto.ObtenerTodo(), "IdProducto", "strNombre");
            ViewData["Producto"] = objeto;

            if (vista == 1)
            {
                modelo = JsonConvert.DeserializeObject<List<clsVentaTemp>>(HttpContext.Session.GetString("ListVentaTemp"));
            }

            HttpContext.Session.SetString("ListVentaTemp", JsonConvert.SerializeObject(modelo));

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTemp(clsVentaTemp _objeto)
        {
            if (_objeto.IdProducto != 0)
            {
                List<clsVentaTemp> modelo = new List<clsVentaTemp>();

                if (HttpContext.Session.GetString("ListVentaTemp") != "[]")
                {
                    modelo = JsonConvert.DeserializeObject<List<clsVentaTemp>>(HttpContext.Session.GetString("ListVentaTemp"));
                }

                _objeto.Producto = await dbProducto.ObtenerPorID(_objeto.IdProducto);

                modelo.Add(_objeto);

                HttpContext.Session.SetString("ListVentaTemp", JsonConvert.SerializeObject(modelo));                
            }
            return RedirectToAction("vVentaTemp", new { vista = 1 });
        }

        //[HttpPost]
        public async Task<IActionResult> GuardarCambiosTemp()
        {

            bool respuesta = false;
            int IdTipoMovimiento = 1;
            int IdTipoOrigen = 2, userId = 1;


            List<clsVentaTemp> modelo = new List<clsVentaTemp>();
            clsVenta venta = new clsVenta();
            clsMovimiento movimiento = new clsMovimiento();

            if (HttpContext.Session.GetString("ListVentaTemp") != "[]")
            {
                modelo = JsonConvert.DeserializeObject<List<clsVentaTemp>>(HttpContext.Session.GetString("ListVentaTemp"));

                venta = await db.Insertar(new clsVenta() { numTotal = modelo.Sum(c => c.Producto.numPrecio), strEstado = "Procesada" });
                movimiento = await dbMovimiento.Insertar(new clsMovimiento() { IdOrigen = venta.IdVenta, IdTipoMovimiento = IdTipoMovimiento, IdTipoOrigen = IdTipoOrigen, UserId = userId });

                foreach (var item in modelo)
                {
                    respuesta = await dbVentaDetalle.Insertar(new clsVentaDetalle() { IdProducto = item.IdProducto, 
                        IdVenta = venta.IdVenta, 
                        numCantidad = item.numCantidad,
                        numPrecio = item.Producto.numPrecio,
                    });

                    await dbMovimientoDetalle.Insertar(new clsMovimientoDetalle()
                    {
                        IdProducto = item.IdProducto,
                        IdMovimiento = movimiento.IdMovimiento,
                        numCantidad = item.numCantidad,
                        numPrecio = item.Producto.numPrecio,
                    }, IdTipoMovimiento);
                }



            }

            

            if (respuesta)
                return RedirectToAction("vVentaTemp");
            else
                return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(clsVenta _objeto)
        {

            bool respuesta;

            if (_objeto.IdVenta == 0)
            {
                respuesta = true;//await db.Insertar(_objeto);
            }
            else
            {
                respuesta = await db.Actualizar(_objeto);
            }

            if (respuesta)
                return RedirectToAction("vVentaIni");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int _IdVenta)
        {

            var respuesta = await db.Eliminar(_IdVenta);

            if (respuesta)
                return RedirectToAction("vVentaIni");
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
