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
    public class DatosSMM_ArmadoPedido
    {
        public int ValidaFolioPrdido(int FolioPed)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMArmadoPedido?IdOrden=" + FolioPed).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public List<SMMProductoArmadoPedido> DatosProductosArmadoPes(string CodBarraProd, int nOrden)
        {
            List<SMMProductoArmadoPedido> ret = new List<SMMProductoArmadoPedido>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMArmadoPedido?Orden=" + nOrden + "&bcdCode=" + CodBarraProd).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<List<SMMProductoArmadoPedido>>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public string insertaRegistroArmadoPedido(int nOrden, string CodProducto, string CodBar, string Umedida, int Cant, string fechIng, int IdVeri, string cantC, string VenC, string EtiqC, string EnfC, string EstC, string CondPC)
        {
            string ret = "";
            try
            {
                string Fvenc = fechIng + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMArmadoPedido?nOrden=" + nOrden + "&CodProducto=" + CodProducto + "&CodBar=" + CodBar + "&Umedida=" + Umedida + "&Cant=" + Cant + "&fechIng=" + Fvenc + "&IdVeri=" + IdVeri + "&cantC=" + cantC + "&VenC=" + VenC + "&EtiqC=" + EtiqC + "&EnfC=" + EnfC + "&EstC=" + EstC + "&CondPC=" + CondPC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return ret;
        }
    }
}
