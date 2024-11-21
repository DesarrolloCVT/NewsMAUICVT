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

        public List<PedidosAsignacion> PedidosAsignados(int transferID)
        {
            List<PedidosAsignacion> rest = new List<PedidosAsignacion>();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("PedidosAsignados?TransferID=" + transferID).Result;
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
    }
}
