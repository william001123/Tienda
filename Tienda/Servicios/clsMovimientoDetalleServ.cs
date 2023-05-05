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
    public class clsMovimientoDetalleServ
        : IServicioMoviDeta<clsMovimientoDetalle, int, List<clsMovimientoDetalle>>
    {

        private static string _baseUrl = "";

        public clsMovimientoDetalleServ(IConfig config)
        {
            _baseUrl = config.baseUrl;
        }

        public async Task<clsMovimientoDetalle> Insertar(clsMovimientoDetalle entidad, int TipoMovimiento)
        {
            clsMovimientoDetalle objeto = new clsMovimientoDetalle();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync($"api/MovimientoDetalle/{TipoMovimiento}", content);

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsMovimientoDetalle>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }

        public async Task<clsMovimientoDetalle> ObtenerPorID(int entidadID)
        {
            clsMovimientoDetalle objeto = new clsMovimientoDetalle();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);            
            var response = await cliente.GetAsync($"api/MovimientoDetalle/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsMovimientoDetalle>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }                
            }

            return objeto;
        }

        public async Task<List<clsMovimientoDetalle>> ObtenerPorIDEnc(int entidadID)
        {
            List<clsMovimientoDetalle> objeto = new List<clsMovimientoDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"api/MovimientoDetalle/ObtenerPorIDEnc/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsMovimientoDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }

        public async Task<List<clsMovimientoDetalle>> ObtenerTodo()
        {
            List<clsMovimientoDetalle> objeto = new List<clsMovimientoDetalle>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/MovimientoDetalle");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsMovimientoDetalle>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }
    }
}
