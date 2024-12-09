using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosUsuario
    {
        public int ObtenerUsuarioStaff(string username)
        {
            if (username != null) 
            {
                int resp = 0;
                try
                {
                    HttpClient ClientHttp = new()
                    {
                        BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                    };
                    var rest2 = ClientHttp.GetAsync("api/Usuario?usernameWMS=" + username).Result;
                    var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<int>(resultadoStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ObtenerUsuarioStaff: " + ex.ToString());
                }
                return resp;
            }
            else
            {
                return -1;
            }
        }
    }
}
