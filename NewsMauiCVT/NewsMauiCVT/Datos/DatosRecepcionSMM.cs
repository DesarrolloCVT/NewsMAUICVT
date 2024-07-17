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
    public class DatosRecepcionSMM
    {
        public List<OcOpenSMM> TraeOcAbiertas()
        {
            List<OcOpenSMM> ls = new List<OcOpenSMM>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<OcOpenSMM>>(resultadoStr);
            }
            catch { }
            return ls;
        }


        public int insertaCabeceraRecepcion(int Orden, int Fac, int bod, string Coment)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?nOC=" + Orden + "&Fac=" + Fac + "&idUser=" + App.Iduser + "&Bod=" + bod + "&Coment=" + Coment).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int ValidaFolioRecepcion(int FolioRecepcion)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?FoliRec=" + FolioRecepcion).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int TraeOcRecepcion(int idRec)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?idRecep=" + idRec).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public List<SMMProductosEnOC> ListaProducOC(int oc, string codba)
        {
            List<SMMProductosEnOC> ret = new List<SMMProductosEnOC>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?OC=" + oc + "&codigo=" + codba).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<List<SMMProductosEnOC>>(resultadoStr);
            }
            catch { }
            return ret;
        }
        public List<SMMProductosEnRecepcionOC> DatosProductosRecepcionXam(string CodBarraProd, int nOrden)
        {
            List<SMMProductosEnRecepcionOC> ret = new List<SMMProductosEnRecepcionOC>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?CodBarraProd=" + CodBarraProd + "&nOrden=" + nOrden).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<List<SMMProductosEnRecepcionOC>>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int insertaDetalleRecepcion(int IdRec, int OC, string CodPro, string Cant, string Prove, string Fvencimiento, string FProduccion, string DunPro)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?recepID=" + IdRec + "&OC=" + OC + "&CodProducto=" + CodPro + "&Cantidad=" + Cant + "&CodProveedor=" + Prove + "&FVenc=" + Fvencimiento + "&FProd=" + FProduccion + "&NumProd=" + DunPro).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int TraeDiasUtilPorc(string CodPro)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?CodProds=" + CodPro).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int TraeDiasUtilTotal(string CodigoProductoEva)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/RecepcionSMM?CodigoProductoEva=" + CodigoProductoEva).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }
    }
}
