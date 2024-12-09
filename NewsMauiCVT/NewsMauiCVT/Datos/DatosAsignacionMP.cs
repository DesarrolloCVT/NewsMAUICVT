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
    internal class DatosAsignacionMP
    {
        public List<TransferenciaAsignacion> TransferenciasAsignadas(int transferID)
        {
            List<TransferenciaAsignacion> rest = new List<TransferenciaAsignacion>();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress= new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("TransfernciasAsignadas?TransferID=" + transferID).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                rest = JsonConvert.DeserializeObject<List<TransferenciaAsignacion>>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TransferenciasAsignadas: " + ex.ToString());
            }
            return rest;
        }
        public DataTable DetalleTransferenciasAsignadas(int transferId)
        {
            DataTable dt = new();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("DetalleTransferenciasAsignadas?TransferID=" + transferId).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DetalleTransferenciasAsignadas: " + ex.ToString());
            }
            return dt;
        }
        public DataTable UbicacionMPAsignacion(string itemCode, string lote)
        {
            DataTable dt = new();
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("UbicacionMPAsignacion?itemCode=" + itemCode + "&lote=" + lote).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("UbicacionMPAsignacion: " + ex.ToString());
            }
            return dt;
        }
    }
}
