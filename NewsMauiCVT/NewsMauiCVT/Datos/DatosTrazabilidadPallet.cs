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
    public class DatosTrazabilidadPallet
    {
        public DatosTrazabilidadPallet() { }

        public List<TrazabilidadPaletClass> BuscaTraabilidadPallet(int npallet)
        {
            List<TrazabilidadPaletClass> dt = new List<TrazabilidadPaletClass>();

            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TrazabilidadPallet?NumPallet=" + npallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<List<TrazabilidadPaletClass>>(resultadoStr) ??
                                throw new InvalidOperationException();


            }
            catch
            {

            }

            return dt;


        }

        public DataTable DetalleTrazabilidadPallet(int npallet)
        {
            DataTable dt = new DataTable();

            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TrazabilidadPallet?NPallet=" + npallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch
            {

            }

            return dt;
        }
    }
}
