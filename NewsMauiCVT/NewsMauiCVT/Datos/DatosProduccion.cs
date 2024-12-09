using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosProduccion
    {
        public bool AddLocation(int PackageId, int LayoutDestinoId, int StaffId)
        {
            bool resp = false;
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("api/Produccion?PackageId=" + PackageId + "&LayoutDestinoId=" + LayoutDestinoId + "&StaffId=" + StaffId).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception) 
            {

            }
            return resp;
        }
        /*public List<Package> ListaInfoPallet(string Package) 
        {
            List<Package> ret = new List<Package>();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("api/Produccion?SSCC=" + Package).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<List<Package>>(resultadoStr);
            }
            catch (Exception) 
            {
            }
            return ret;
        }*/
        public int CantidadPorLoteOrderDetail(int order_id, string lote)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("ObtieneOrderDetail?orderID=" + order_id + "&lote=" + lote).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception) 
            {

            }
            return ret;
        }
    }
}
