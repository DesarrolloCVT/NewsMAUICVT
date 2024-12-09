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
    public class DatosTipoPallet
    {
        public List<TipoPalletClass> ListaTipoPallet()
        {
            List<TipoPalletClass> ls = new List<TipoPalletClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Supportive").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<TipoPalletClass>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)  
            {
                Console.WriteLine("ListaTipoPallet: " + ex.ToString());
            }
            return ls;
        }
        public int TraeIdTipoPallet(string Decripcion)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Supportive?Desc=" + Decripcion).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeIdTipoPallet: " + ex.ToString());
            }
            return ret;
        }
        public bool ActualizaTipoPallet(int SSCC, int CodTipo)
        {
            bool ret = false;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Supportive?nPallet=" + SSCC + "&TipoPallet=" + CodTipo).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ActualizaTipoPallet: " + ex.ToString());
            }
            return ret;
        }
        public string TraeNombreTipo(int pk)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Supportive?Pk=" + pk).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeNombreTipo: " + ex.ToString());
            }
            return ret;
        }
    }
}
