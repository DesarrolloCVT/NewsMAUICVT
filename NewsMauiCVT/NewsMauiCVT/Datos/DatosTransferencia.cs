using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosTransferencia
    {
        public bool InsertaTransferencia(int transferid, int packageid)
        {
            bool resp = false;
            try
            {
                string username = App.UserSistema;
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("api/Bodega?transferid=" + transferid + "&packageid=" + packageid + "&username=" + username).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
                if (rest.IsSuccessStatusCode)
                {
                    resp = true;
                }
            }
            catch (Exception ex)
            {   
                Console.WriteLine("InsertaTransferencia: " + ex.Message);
            }
            return resp;
        }
    }
}
