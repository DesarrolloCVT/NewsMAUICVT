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
    public class DatosPosicionamientoSMM
    {
        public List<ListProdTranferSMMClass> ObtienedatosProducto(string SSCC)
        {
            List<ListProdTranferSMMClass> ls = new List<ListProdTranferSMMClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?NumeroPalletPos=" + SSCC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<ListProdTranferSMMClass>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ObtienedatosProducto: " + ex.Message);
            }
            return ls;
        }
        public List<SMMPackageClass> ObtieneInfoPalletPosicionamientoSMM(string SSCC)
        {
            List<SMMPackageClass> ls = new List<SMMPackageClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?SSCC=" + SSCC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMPackageClass>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ObtieneInfoPalletPosicionamientoSMM: " + ex.Message);
            }
            return ls;
        }

        public int ObtienePackageIdPosicionamiento(string NumPallet)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?NumPallet=" + NumPallet).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ObtienePackageIdPosicionamiento: " + ex.Message);
            }
            return ret;
        }

        public string TraeLayoutDesc(int codLay)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?idLayout=" + codLay).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeLayoutDesc: " + ex.Message);
            }
            return ret;
        }

        public string TraeBodegaxLayout(int codLay)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?idPosicion=" + codLay).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeBodegaxLayout: " + ex.Message);
            }
            return ret;
        }

        public bool ActualizaLayoutPackage(int PackageId, int layoutid)
        {
            bool ret = false;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?PackageId=" + PackageId + "&layoutid=" + layoutid).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ActualizaLayoutPackage: " + ex.Message);
            }
            return ret;
        }

        public bool AddLocation(int PackageId, int LayoutDestinoId, int idUsuario)
        {
            bool ret = false;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/PosicionamientoSMM?PackageId=" + PackageId + "&LayoutDestinoId=" + LayoutDestinoId + "&idUsuario=" + idUsuario).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddLocation: " + ex.Message);
            }
            return ret;
        }
    }
}
