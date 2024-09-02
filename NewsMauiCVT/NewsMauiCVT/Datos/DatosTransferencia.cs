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
        public int InsertaTransferencia(int transferid, int packageid)
        {
            int resp = 0;

            try
            {
                string username = App.UserSistema;
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("api/Bodega?transferid=" + transferid + "&packageid=" + packageid + "&username=" + username).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<int>(resultadoStr);

            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertaTransferencia: " + ex.Message);
            }
            return resp;

        }
    }
}
