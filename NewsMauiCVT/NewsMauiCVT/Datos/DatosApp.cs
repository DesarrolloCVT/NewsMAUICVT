using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosApp
    {
        public string TraeVersion(string? vers)
        {
            string Vers = "0.0.0";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest2 = ClientHttp.GetAsync("api/DatosAPP?idAPP=" + 1).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                Vers = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch (Exception ex)
            {
                string mns = ex.Message;

            }

            return vers;
        }

        public List<MenuClass> TraeMenu(int idperfil)
        {
            List<MenuClass> ls = new List<MenuClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Usuario?idPerfilUsuario=" + idperfil).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                ls = JsonConvert.DeserializeObject<List<MenuClass>>(resultadoStr);
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            }
            catch { }
            return ls;
        }
    }
}
