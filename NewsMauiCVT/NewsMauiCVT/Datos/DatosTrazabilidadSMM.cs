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
    public class DatosTrazabilidadSMM
    {

        public DataTable DetalleTrazaSMM(string PalletTraza)
        {
            DataTable dt = new DataTable();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TrazabilidadSMM?PalletTraza=" + PalletTraza).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DetalleTrazaSMM: " + ex.Message);
            }

            return dt;
        }

        public List<SMMTrazabilidadBusqueda> ObtienedatosTraza(string SSCC)
        {
            List<SMMTrazabilidadBusqueda> ls = new List<SMMTrazabilidadBusqueda>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TrazabilidadSMM?NumPallet=" + SSCC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMTrazabilidadBusqueda>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ObtienedatosTraza: " + ex.Message);
            }
            return ls;
        }

    }
}
