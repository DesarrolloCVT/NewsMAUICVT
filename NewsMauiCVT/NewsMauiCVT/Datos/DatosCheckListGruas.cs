using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosCheckListGruas
    {
        public List<string> TipoMaquinarias()
        {
            List<string> tipos = [];
            try
            {
                HttpClient HttpClient = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = HttpClient.GetAsync("api/CheckListGruas/TiposMaquinaria").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                tipos = JsonConvert.DeserializeObject<List<string>>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatosMaquinarias: " + ex.ToString());
            }
            return tipos;
        }

        public List<string> NumeroGruas()
        {
            List<string> numeroGrua = [];
            try
            {
                HttpClient HttpClient = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = HttpClient.GetAsync("api/CheckListGruas/NumeroGrua").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                numeroGrua = JsonConvert.DeserializeObject<List<string>>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatosMaquinarias: " + ex.ToString());
            }
            return numeroGrua;
        }
    }
}
