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
    public class clsEmpleadoServ
        : IServicioBase<clsEmpleado, int>
    {

        private static string _baseUrl = "";

        public clsEmpleadoServ(IConfig config)
        {
            _baseUrl = config.baseUrl;
        }

        public async Task<bool> Actualizar(clsEmpleado entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("api/Empleado", content);

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

            var response = await cliente.DeleteAsync($"api/Empleado/{entidadID}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Insertar(clsEmpleado entidad)
        {
            bool respuesta = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Empleado", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<clsEmpleado> ObtenerPorID(int entidadID)
        {
            clsEmpleado objeto = new clsEmpleado();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);            
            var response = await cliente.GetAsync($"api/Empleado/{entidadID}");

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<clsEmpleado>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }                
            }

            return objeto;
        }

        public async Task<List<clsEmpleado>> ObtenerTodo()
        {
            List<clsEmpleado> objeto = new List<clsEmpleado>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync("api/Empleado");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<clsEmpleado>>(json_respuesta);

                if (resultado != null)
                {
                    objeto = resultado;
                }
            }

            return objeto;
        }
    }
}
