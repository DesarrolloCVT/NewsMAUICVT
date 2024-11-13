using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosAsignacionMP
    {
        public bool InsertarAsignacionMP(int transferID, string itemCode, string lote)
        {
            bool resp = false;
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("InsertaMP?transferID=" + transferID + "&itemCode=" + itemCode + "&Lote=" + lote).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertarAsignacionMP: " + ex.ToString());
            }
            return resp;
        }
    }
}
