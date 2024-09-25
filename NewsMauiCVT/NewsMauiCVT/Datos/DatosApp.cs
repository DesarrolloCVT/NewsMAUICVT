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
        public string TraeVersion()
        {
            string vers = "0.0.0";
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };

                var rest2 = ClientHttp.GetAsync("api/DatosAPP?idAPP=" + 1).Result;
                if (rest2.IsSuccessStatusCode)
                {
                    var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                    vers = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                        throw new InvalidOperationException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Version: " + vers);
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
                ls = JsonConvert.DeserializeObject<List<MenuClass>>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("TraeMenu: " + ex.ToString());
            }
            return ls;
        }

        public List<DatosMaquinarias> DatosMaquinarias(int id) 
        {
            List<DatosMaquinarias> maquinarias = new List<DatosMaquinarias>();
            try
            {
                HttpClient HttpClient = new HttpClient();
                HttpClient.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = HttpClient.GetAsync("DatosAPP?idMaquinaria=" + id).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                maquinarias = JsonConvert.DeserializeObject<List<DatosMaquinarias>>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatosMaquinarias: " + ex.ToString());
            }
            return maquinarias;
        }
    }
}
