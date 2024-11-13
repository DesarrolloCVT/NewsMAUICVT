using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosAsignacionPedidos
    {
        public bool InsertarAsignacionPedidos(int orderid, string itemcode, string lote, int cantidad)
        {
            bool resp = false;
            try
            {
                HttpClient HttpClient = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = HttpClient.GetAsync("InsertaPedido?orderID=" + orderid + "&itemCode=" + itemcode + "&Lote=" + lote + "&Cantidad=" + cantidad).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertarAsignacionPedidos: " + ex.ToString());
            }
            return resp;
        }
    }
}
