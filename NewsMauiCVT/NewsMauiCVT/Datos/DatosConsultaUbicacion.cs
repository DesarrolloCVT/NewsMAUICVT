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
    public class DatosConsultaUbicacion
    {
        public DatosConsultaUbicacion() { }


        public int EvaluaExistenDatosEnPosision(int idpos)
        {
            int Estado = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest2 = ClientHttp.GetAsync("api/Produccion?Idpos=" + idpos).Result;

                if (rest2.IsSuccessStatusCode)
                {
                    var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
                    if (resultadoStr.Equals("[]"))
                    {
                        Estado = 0;
                    }
                    else
                    {
                        Estado = 1;
                    }
                }
            }
            catch { }

            return Estado;
        }

        public DataTable DetalleConsultaUbicacion(int npallet)
        {
            DataTable dt = new DataTable();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                int idpos = Convert.ToInt32(npallet);
                var rest2 = ClientHttp.GetAsync("api/Produccion?Idpos=" + idpos).Result;

                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch
            {

            }

            return dt;

        }

        public DataTable ResumenConsultaUbicacion(int npallet)
        {
            DataTable dt = new DataTable();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                int idpos = Convert.ToInt32(npallet);
                var rest2 = ClientHttp.GetAsync("api/Produccion?NumPosicion=" + idpos).Result;

                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
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
