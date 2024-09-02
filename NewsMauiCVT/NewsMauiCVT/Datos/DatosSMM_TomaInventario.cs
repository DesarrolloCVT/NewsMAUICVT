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
    public class DatosSMM_TomaInventario
    {
        public List<TomaInventarioClass> TraeFoliosInventarios()
        {
            List<TomaInventarioClass> ls = new List<TomaInventarioClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<TomaInventarioClass>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeFoliosInventarios: " + ex.Message);
            }
            return ls;
        }


        public int ValidaBodegaSMM(string CodBodega)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodBodega=" + CodBodega).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ValidaBodegaSMM: " + ex.Message);
            }
            return ret;
        }

        public string ValidaCodProducto(string codProd)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodBarraProd=" + codProd).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ValidaCodProducto: " + ex.Message);
            }
            return ret;
        }
        public int ValidaNumeroDePallet(int NumPallet, string Codpro)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?NumeroDePallet=" + NumPallet + "&Codpro=" + Codpro).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ValidaNumeroDePallet: " + ex.Message);
            }
            return ret;
        }
        public int ValidaPallet(int PalletVerifica)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?PalletVerifica=" + PalletVerifica).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ValidaPallet: " + ex.Message);
            }
            return ret;
        }

        public string traeItemCodeProducto(string PalletProducto)
        {
            string ret = "0";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?PalletProducto=" + PalletProducto).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("traeItemCodeProducto: " + ex.Message);
            }
            return ret;
        }

        public string traeCodBarraProducto(string CodProductoBarra)
        {
            string ret = "0";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodProductoBarra=" + CodProductoBarra).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("traeCodBarraProducto: " + ex.Message);
            }
            return ret;
        }
        public string traeCodBarraProductoBodega02(string NpalletBodega02)
        {
            string ret = "0";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?NpalletBodega02=" + NpalletBodega02).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("traeCodBarraProductoBodega02: " + ex.Message);
            }
            return ret;
        }
        public int ValidaUbicacion(int Ubicacion, int site)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?bodega=" + site + "&ubicacion=" + Ubicacion).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ValidaUbicacion: " + ex.Message);
            }
            return ret;
        }
        public string TraeCodProducti(string codBar)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodBarr=" + codBar).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeCodProducti: " + ex.Message);
            }
            return ret;
        }


        public bool insertaInventario(int Inventario_Id, string Dun14, string CodProducto, string Cantidad, int SiteID, int IdUsuario, string UbiPasillo, string Fvencimiento, string CantxEmp, int CantBase, string TipoEmpaque)
        {
            bool ret = false;
            try
            {
                string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?vInventario_Id=" + Inventario_Id + "&vDun14=" + Dun14 + "&vCodProducto=" + CodProducto + "&vCantidad=" + Cantidad + "&vSiteID=" + SiteID + "&vIdUsuario=" + IdUsuario + "&vUbiPasillo=" + UbiPasillo + "&Fvenc=" + Fvenc + "&vCantxEmp=" + CantxEmp + "&vCantBase=" + CantBase + "&vTipoEmpaque=" + TipoEmpaque).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("insertaInventario: " + ex.Message);
            }
            return ret;
        }
        public bool insertaDetalleInventarioBodegas(int Inventario_Id, string Dun14, string CodProducto, string Cantidad, int SiteID, int IdUsuario, int UbiPasillo, string Fvencimiento, int SSCC, string CantxEmp, int CantBase, string TipoEmpaque)
        {
            bool ret = false;
            try
            {
                string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?vInventario_Id=" + Inventario_Id + "&vDun14=" + Dun14 + "&vCodProducto=" + CodProducto + "&vCantidad=" + Cantidad + "&vSiteID=" + SiteID + "&vIdUsuario=" + IdUsuario + "&vUbicacion=" + UbiPasillo + "&Fvenc=" + Fvenc + "&SSCC=" + SSCC + "&vCantxEmp=" + CantxEmp + "&vCantBase=" + CantBase + "&vTipoEmpaque=" + TipoEmpaque).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("insertaDetalleInventarioBodegas: " + ex.Message);
            }
            return ret;
        }

        public List<SMMDatoProductosRecepcion> ListaDatosProdRes(string itemcode, string codBar)
        {
            List<SMMDatoProductosRecepcion> ls = new List<SMMDatoProductosRecepcion>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync(" api/TomaInventarioSMM?itemcode=" + itemcode + "&codBar=" + codBar).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMDatoProductosRecepcion>>(resultadoStr) ?? throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ListaDatosProdRes: " + ex.Message);
            }
            return ls;
        }

    }
}
