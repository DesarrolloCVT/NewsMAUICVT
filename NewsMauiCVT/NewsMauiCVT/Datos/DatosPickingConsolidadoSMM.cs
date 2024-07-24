using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosPickingConsolidadoSMM
    {
        public DatosPickingConsolidadoSMM()
        {

        }

        public int insertaRegistro(string FConsolidado, string CodProducto, int Cantidad, string Depto)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PikingConsolidadoSMM?FConsolidado=" + FConsolidado + "&CodProducto=" + CodProducto + "&Cantidad=" + Cantidad + "&idUser=" + App.Iduser + "&Depto=" + Depto).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int ValidaProductoConsolidado(string CodProd, string fechaCons)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PikingConsolidadoSMM?CodProducto=" + CodProd + "&fechaCons=" + fechaCons).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public DataTable DetallePickingSMM(string fecha, string dpto)
        {
            DataTable dt = new DataTable();

            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/PikingConsolidadoSMM?fecha=" + fecha + "&dpto=" + dpto).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();

            }
            catch
            {

            }

            return dt;

        }

        public List<SMMDepartamentos> TraeDepartamentos()
        {
            List<SMMDepartamentos> ls = new List<SMMDepartamentos>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PikingConsolidadoSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMDepartamentos>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch { }
            return ls;
        }
    }
}
