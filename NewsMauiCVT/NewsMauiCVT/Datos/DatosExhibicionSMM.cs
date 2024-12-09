using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosExhibicionSMM
    {
        public bool InsertaRegistroExhibicion(string pasillo, int columna, int nivel, string CodBar, string codProd, int CantCaras)
        {
            bool ret = false;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ExhibicionSalaSMM?pasillo=" + pasillo + "&columna=" + columna + "&nivel=" + nivel + "&CodBar=" + CodBar + "&codProd=" + codProd + "&CantCaras=" + CantCaras).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("InsertaRegistroExhibicion: " + ex.Message);
            }
            return ret;
        }
    }
}
