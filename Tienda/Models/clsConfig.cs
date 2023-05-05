using Tienda.Interfaces;

namespace Tienda.Models
{
    public class clsConfig : IConfig
    {
        public string baseUrl { get; set; }

        public clsConfig() {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;

        }

    }
}
