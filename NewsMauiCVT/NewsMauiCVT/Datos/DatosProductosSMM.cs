using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosProductosSMM
    {
        public string ValidaProductoSMM(string codProd)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ConsultaProductosSMM?BarraProd=" + codProd).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public List<ValidadorProductosSMMClass> ListDatoProductosSMMValida(string nBarra)
        {
            List<ValidadorProductosSMMClass> dt = new List<ValidadorProductosSMMClass>();

            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/ConsultaProductosSMM?NumBarra=" + nBarra).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<List<ValidadorProductosSMMClass>>(resultadoStr) ??
                                throw new InvalidOperationException();


            }
            catch
            {

            }

            return dt;

        }
    }
}
