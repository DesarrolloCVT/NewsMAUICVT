using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosSMM_Etiquetas
    {
        public string insertaRegEtiPrec(string CodProd)
        {
            string ret = "-1";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMRegEtiquetas?codProd=" + CodProd).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

        public string insertaRegEtiqSala(string codProd, string DetProd, int cantidad, string fVenci)
        {
            string ret = "-1";
            try
            {
                // string Fvenc = fVenci + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMRegEtiquetas?codProd=" + codProd + "&DetProd=" + DetProd + "&cantidad=" + cantidad + "&fVenci=" + fVenci).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }
    }
}
