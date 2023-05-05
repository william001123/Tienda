using System.Net.Http.Headers;
using System.Text;
using System;
using Tienda.Interfaces.Servicios;
using Tienda.Models;
using Newtonsoft.Json;
using Tienda.Interfaces;
using Newtonsoft.Json.Linq;

namespace Tienda.Servicios
{
    public class clsProductoServ
        : IServicioBase<clsProducto, int>
    {

        private static string _baseUrl = "";

        public clsProductoServ(IConfig config)
        {
            _baseUrl = config.baseUrl;
        }

        public async Task<bool> Actualizar(clsProducto entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/Producto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int entidadID)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);          

            var response = await cliente.DeleteAsync($"api/Producto/{entidadID}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Insertar(clsProducto entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Producto", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<clsProducto> ObtenerPorID(int entidadID)
        {
            clsProducto objeto = new clsProducto();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);            
            var response = await cliente.GetAsync($"api/Producto/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsProducto>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }                
            }

            return objeto;
        }

        public async Task<List<clsProducto>> ObtenerTodo()
        {
            List<clsProducto> objeto = new List<clsProducto>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/Producto");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsProducto>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }
    }
}
