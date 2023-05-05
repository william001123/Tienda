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
    public class clsVentaDetalleServ
        : IServicioObteIDEnc<clsVentaDetalle, int, List<clsVentaDetalle>>
    {

        private static string _baseUrl = "";

        public clsVentaDetalleServ(IConfig config)
        {
            _baseUrl = config.baseUrl;
        }

        public async Task<bool> Actualizar(clsVentaDetalle entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/VentaDetalle", content);

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

            var response = await cliente.DeleteAsync($"api/VentaDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Insertar(clsVentaDetalle entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/VentaDetalle", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<clsVentaDetalle> ObtenerPorID(int entidadID)
        {
            clsVentaDetalle objeto = new clsVentaDetalle();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);            
            var response = await cliente.GetAsync($"api/VentaDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsVentaDetalle>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }                
            }

            return objeto;
        }

        public async Task<List<clsVentaDetalle>> ObtenerPorIDEnc(int entidadID)
        {
            List<clsVentaDetalle> objeto = new List<clsVentaDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"api/VentaDetalle/ObtenerPorIDEnc/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsVentaDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }

        public async Task<List<clsVentaDetalle>> ObtenerTodo()
        {
            List<clsVentaDetalle> objeto = new List<clsVentaDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/VentaDetalle");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsVentaDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }
    }
}
