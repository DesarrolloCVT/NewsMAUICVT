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
        public bool InsertarAsignacionMP(int transferID, string itemCode, string lote)
        {
            bool resp = false;
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest2 = ClientHttp.GetAsync("InsertaMP?transferID=" + transferID + "&itemCode=" + itemCode + "&Lote=" + lote).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertarAsignacionMP: " + ex.ToString());
            }
            return resp;
        }

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
                var rest2 = ClientHttp.GetAsync("DetalleTransferenciasAsignadas?transferId=" + transferId).Result;
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
    }
}
