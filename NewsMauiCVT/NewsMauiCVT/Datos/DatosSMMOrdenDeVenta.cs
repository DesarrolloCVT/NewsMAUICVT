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
    public class DatosSMMOrdenDeVenta
    {
        public int ValidaFolioOrden(int FolioOrden)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM?FolOrden=" + FolioOrden).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public string ValidaRutCliente(string RutCliente)
        {
            string ret = "-1";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM?Rutcli=" + RutCliente).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch { }
            return ret;
        }

        public bool insertacliente(string rutClien, string nomCliente, string Razon, string Giro, string dirFac, string dirDes, string Tel, string mail, string lati, string longitud, string alt)
        {
            bool ret = false;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM?rutClien=" + rutClien + "&nomCliente=" + nomCliente + "&Razon=" + Razon + "&Giro=" + Giro + "&dirFac=" + dirFac + "&dirDes=" + dirDes + "&Tel=" + Tel + "&mail=" + mail + "&lati=" + lati + "&longitud=" + longitud + "&alt=" + alt).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public List<SMMClienteOrdenDeVentas> TraeClientesOrden()
        {
            List<SMMClienteOrdenDeVentas> ls = new List<SMMClienteOrdenDeVentas>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMClienteOrdenDeVentas>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch { }
            return ls;
        }

        public List<SMMDireccionOrdenDeVentas> TraeDirDespachoOrden(string CodCliente)
        {
            List<SMMDireccionOrdenDeVentas> ls = new List<SMMDireccionOrdenDeVentas>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM?CodCliente=" + CodCliente).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMDireccionOrdenDeVentas>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch { }
            return ls;
        }
        public List<SMMDireccionFactOrdenVentas> TraeDirFacturacion(string DirClienteFactura)
        {
            List<SMMDireccionFactOrdenVentas> ls = new List<SMMDireccionFactOrdenVentas>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM?DirClienteFactura=" + DirClienteFactura).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMDireccionFactOrdenVentas>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch { }
            return ls;
        }

        public int CreaOrdenVenta(string NumCliente, string fechaEnt, string direcDesp, string direcFact)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaSMM?NumCliente=" + NumCliente + "&fechaEnt=" + fechaEnt + "&direcDesp=" + direcDesp + "&direcFact=" + direcFact).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public List<SMMProductosOrdenDeVenta> ListaProductosOrdenVenta()
        {
            List<SMMProductosOrdenDeVenta> ls = new List<SMMProductosOrdenDeVenta>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaDetalleSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMProductosOrdenDeVenta>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch { }
            return ls;
        }

        public int insertaDetalleOrden(int idOrden, string codProd, int cantidad, int PorcDesc)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaDetalleSMM?idOrden=" + idOrden + "&codProd=" + codProd + "&cantidad=" + cantidad + "&PorcDesc=" + PorcDesc).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public DataTable RegistroOrdenVEnta(int idFolio)
        {
            DataTable dt = new DataTable();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");


                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaDetalleSMM?idFolio=" + idFolio).Result;

                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch
            {

            }

            return dt;
        }

        public List<TotalesOrden> DatosTotales(int idOrdendeVenta)
        {
            List<TotalesOrden> ls = new List<TotalesOrden>();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");


                var rest2 = ClientHttp.GetAsync("api/OrdenDeVentaDetalleSMM?idOrdendeVenta=" + idOrdendeVenta).Result;

                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<TotalesOrden>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch
            {

            }

            return ls;
        }

        //public class Totales
        //{
        //    public int Subtotal { get; set; }
        //    public int Impto { get; set; }
        //    public int Total { get; set; }
        //}


    }
}
