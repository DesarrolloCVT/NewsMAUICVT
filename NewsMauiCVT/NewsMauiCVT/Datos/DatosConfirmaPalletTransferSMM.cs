using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosConfirmaPalletTransferSMM
    {
        public int ConfirmaFolio(int TransferID)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ConfirmaPalletTransferenciaSMM?TransferID=" + TransferID).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("ConfirmaFolio: " + ex.Message);
            }
            return ret;
        }
        public int ConfirmaPallet(int TransferID, int idBulto)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ConfirmaPalletTransferenciaSMM?TransferID=" + TransferID + "&idBulto=" + idBulto).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("ConfirmaPallet: " + ex.Message);
            }
            return ret;
        }
        public int ActualizaConfirma(int PackageId, int transfer)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ConfirmaPalletTransferenciaSMM?PackageId=" + PackageId + "&transfer=" + transfer).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("ActualizaConfirma: " + ex.Message);
            }
            return ret;
        }
    }
}
