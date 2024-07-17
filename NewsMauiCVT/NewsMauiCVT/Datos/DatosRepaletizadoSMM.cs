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
    public class DatosRepaletizadoSMM
    {
        public DatosRepaletizadoSMM() { }


        public List<SMMPackageClass> ObtieneDatosPaletSMM(int Npallet)
        {
            List<SMMPackageClass> ls = new List<SMMPackageClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?pkg=" + Npallet).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMPackageClass>>(resultadoStr);
            }
            catch { }
            return ls;
        }


        public string ObtieneNombreProducSMM(string itemC)
        {
            string res = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?itemCod=" + itemC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return res;

        }

        public int ObtienSiteLayoutSMM(int LayId)
        {
            int res = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?layoutid=" + LayId).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return res;
        }

        public string ObtieneNombreSitio(int site)
        {
            string res = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?siteid=" + site).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return res;

        }

        public int VerificaReservaSMM(string SSCC)
        {
            int res = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?SSCC=" + SSCC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return res;

        }

        public bool RepaletizaDestino(string Origen, string Destino, string Cant, int username)
        {
            bool res = false;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?Origen=" + Origen + "&Destino=" + Destino + "&Cant=" + Cant + "&username=" + username).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch { }
            return res;
        }

        public string RepaletizaNuevo(string Origen, string Cantidad, int user)
        {
            string res = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RepaletizadoSMM?Origen=" + Origen + "&Cantidad=" + Cantidad + "&user=" + user).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return res;

        }
    }
}
