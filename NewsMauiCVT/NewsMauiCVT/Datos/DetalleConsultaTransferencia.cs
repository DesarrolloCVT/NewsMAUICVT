using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DetalleConsultaTransferencia
    {
        public DataTable DetalleConsultaTransferencias(int npallet)
        {
            DataTable dt = new();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                };
                int folio = Convert.ToInt32(npallet);
                var rest2 = ClientHttp.GetAsync("api/Produccion?Idpos=" + folio).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DetalleConsultaUbicacion: " + ex.ToString());
            }
            return dt;
        }
    }
}
