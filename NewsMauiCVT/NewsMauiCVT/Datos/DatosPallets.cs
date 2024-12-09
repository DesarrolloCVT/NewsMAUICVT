using DevExpress.Entity.Model.Metadata;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosPallets
    {
        public DatosPallets() 
        {

        }
        public List<PalletClass> ObtieneInfoPallet(string NPallet)
        {
            List<PalletClass> list = [];
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("api/Produccion?SSCC=" + NPallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<PalletClass>>(resultadoStr) ??
                                throw new InvalidOperationException();

            }
            catch (Exception ex) 
            {
                Console.WriteLine("ObtieneInfoPallet: " + ex.ToString());
            }
            return list;
        }
        public List<string> ObtieneDatos(string NPallet) 
        {
            List<string> list = [];

            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("ObtieneDatos?packageSSCC=" + NPallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<string>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ObtieneDatos: " + ex.ToString());
            }
            return list;
        }
        public bool ValidaPallet(string NPallet)
        {
            bool value = false;

            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("ValidaPallet?SSCC=" + NPallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                value = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ObtieneDatos: " + ex.ToString());
            }
            return value;
        }
    }
}
