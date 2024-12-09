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
    public class DatosFefoStock
    {
        public DatosFefoStock() { }
        public DataTable Sp_Fefo(int IdBodega, string CodProducto)
        {
            DataTable dt = new DataTable();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync(" api/Stock?IdBodega=" + IdBodega + "&CodProducto=" + CodProducto).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Sp_Fefo: " + ex.Message);
            }
            return dt;
        }
        public int TraeIdBodega(string descrip)
        {
            int res = 0;
            try
            {
                List<Site> lsBod = DatosBodegas();

                foreach (var t in lsBod)
                {
                    if (t.Site_Description == descrip)
                    {
                        res = t.Site_Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TraeIdBodega: " + ex.Message);
            }
            return res;
        }
        public List<Site> DatosBodegas()
        {
            List<Site> lt = new List<Site>();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Bodega").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                lt = JsonConvert.DeserializeObject<List<Site>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DatosBodegas: {ex.Message}");
            }

            return lt;
        }
        public string TraeCodProd(string nompro)
        {
            string res = "";
            try
            {
                List<Productos> lsProd = DatosProductos();

                foreach (var t in lsProd)
                {
                    if (t.ArticleProvider_Description == nompro)
                    {
                        res = t.ArticleProvider_CodClient;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("TraeCodProd: " + ex.Message);
            }
            return res;
        }
        public List<Productos> DatosProductos()
        {
            List<Productos> lt = new List<Productos>();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Produccion").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                lt = JsonConvert.DeserializeObject<List<Productos>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatosProductos: " + ex.Message);
            }

            return lt;
        }
    }
}
