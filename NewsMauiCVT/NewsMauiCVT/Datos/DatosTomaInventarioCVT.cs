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
    public class DatosTomaInventarioCVT
    {
        public List<ProductoInventarioCVT> ListProductosInventario(int nPallet)
        {
            List<ProductoInventarioCVT> dt = new List<ProductoInventarioCVT>();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TomaInventario?PalletInv=" + nPallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<List<ProductoInventarioCVT>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch
            {

            }

            return dt;

        }

        public int traeSiteIdLayout(int layoutid)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Bodega?layoutid=" + layoutid).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }
    }
}
