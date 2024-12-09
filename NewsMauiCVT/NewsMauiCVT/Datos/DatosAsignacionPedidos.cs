using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosAsignacionPedidos
    {
        public List<PedidosAsignacion> PedidosAsignados(int transferID)
        {
            List<PedidosAsignacion> rest = new List<PedidosAsignacion>();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("PedidosAsignados?OrderID=" + transferID).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                rest = JsonConvert.DeserializeObject<List<PedidosAsignacion>>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PedidosAsignados: " + ex.ToString());
            }
            return rest;
        }
        public DataTable DetallePedidosAsignados(int orderID)
        {
            DataTable dt = new();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("DetallePedidosAsignados?orderID=" + orderID).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DetallePedidosAsignados: " + ex.ToString());
            }
            return dt;
        }
        public DataTable UbicacionPedidoAsignacion(string itemCode, string lote)
        {
            DataTable dt = new();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("UbicacionPedidoAsignacion?itemCode=" + itemCode + "&lote=" + lote).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("UbicacionPedidoAsignacion: " + ex.ToString());
            }
            return dt;
        }
    }
}
