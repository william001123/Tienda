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
    public class clsRecepcionDetalleServ
        : IServicioObteIDEnc<clsRecepcionDetalle, int, List<clsRecepcionDetalle>>
    {

        private static string _baseUrl = "";

        public clsRecepcionDetalleServ(IConfig config)
        {
            _baseUrl = config.baseUrl;
        }

        public async Task<bool> Actualizar(clsRecepcionDetalle entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/RecepcionDetalle", content);

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

            var response = await cliente.DeleteAsync($"api/RecepcionDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Insertar(clsRecepcionDetalle entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/RecepcionDetalle", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<clsRecepcionDetalle> ObtenerPorID(int entidadID)
        {
            clsRecepcionDetalle objeto = new clsRecepcionDetalle();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);            
            var response = await cliente.GetAsync($"api/RecepcionDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsRecepcionDetalle>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }                
            }

            return objeto;
        }

        public async Task<List<clsRecepcionDetalle>> ObtenerPorIDEnc(int entidadID)
        {
            List<clsRecepcionDetalle> objeto = new List<clsRecepcionDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"api/RecepcionDetalle/ObtenerPorIDEnc/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsRecepcionDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }

        public async Task<List<clsRecepcionDetalle>> ObtenerTodo()
        {
            List<clsRecepcionDetalle> objeto = new List<clsRecepcionDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/RecepcionDetalle");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsRecepcionDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }
    }
}
