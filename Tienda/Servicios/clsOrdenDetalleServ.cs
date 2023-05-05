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
    public class clsOrdenDetalleServ
        : IServicioObteIDEnc<clsOrdenDetalle, int, List<clsOrdenDetalle>>
    {

        private static string _baseUrl = "";

        public clsOrdenDetalleServ(IConfig config)
        {
            _baseUrl = config.baseUrl;
        }

        public async Task<bool> Actualizar(clsOrdenDetalle entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/OrdenDetalle", content);

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

            var response = await cliente.DeleteAsync($"api/OrdenDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Insertar(clsOrdenDetalle entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/OrdenDetalle", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<clsOrdenDetalle> ObtenerPorID(int entidadID)
        {
            clsOrdenDetalle objeto = new clsOrdenDetalle();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);            
            var response = await cliente.GetAsync($"api/OrdenDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsOrdenDetalle>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }                
            }

            return objeto;
        }

        public async Task<List<clsOrdenDetalle>> ObtenerPorIDEnc(int entidadID)
        {
            List<clsOrdenDetalle> objeto = new List<clsOrdenDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"api/OrdenDetalle/ObtenerPorIDEnc/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsOrdenDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }

        public async Task<List<clsOrdenDetalle>> ObtenerTodo()
        {
            List<clsOrdenDetalle> objeto = new List<clsOrdenDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/OrdenDetalle");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsOrdenDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }
    }
}
