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
    public class DatosTransferenciaSMM
    {
        public DatosTransferenciaSMM() { }

        public List<ListProdTranferSMMClass> ObtieneDatosPalletSMM(int nPallet)
        {
            List<ListProdTranferSMMClass> lt = new List<ListProdTranferSMMClass>();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?NumPallet=" + nPallet).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                lt = JsonConvert.DeserializeObject<List<ListProdTranferSMMClass>>(resultadoStr);
            }
            catch
            {

            }

            return lt;

        }


        public int InsertaTransferenciaSMM(string siteOrig, string siteDest, int Usuario, int pgID, int lyID, string cantidad)
        {
            int resp = 0;


            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?siteOrig=" + siteOrig + "&siteDest=" + siteDest + "&Usuario=" + Usuario + "&pgID=" + pgID + "&lyID=" + lyID + "&cantidad=" + cantidad).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<int>(resultadoStr);

            }
            catch { }
            return resp;

        }

        public int ValidaTransferencia(int TransferID, int stado)
        {
            int resp = 0;


            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?TransferID=" + TransferID + "&stado=" + stado).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<int>(resultadoStr);

            }
            catch { }
            return resp;

        }

        public List<SMMSite> ListaBodegas()
        {
            List<SMMSite> ls = new List<SMMSite>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMSite>>(resultadoStr);
            }
            catch { }
            return ls;
        }

        public List<SMMFolioSolicitud> ListadoFolios(int estado)
        {
            List<SMMFolioSolicitud> ls = new List<SMMFolioSolicitud>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?Estado=" + estado).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMFolioSolicitud>>(resultadoStr);
            }
            catch { }
            return ls;
        }

        public int InsertaTransferenciaCab(int sOrigen, int sDestino, int idUser, int EntidadT, int FolioEnt, string Coment, int FolioSol)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?sOrigen=" + sOrigen + "&sDestino=" + sDestino + "&idUser=" + idUser + "&EntidadT=" + EntidadT + "&FolioEnt=" + FolioEnt + "&Coment=" + Coment + "&FolioSol=" + FolioSol).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public List<FiltoTransferenciaSMM> FiltroTransferencia(int NPallet, int TransferId)
        {
            List<FiltoTransferenciaSMM> ls = new List<FiltoTransferenciaSMM>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?NPallet=" + NPallet + "&TransferId=" + TransferId).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<FiltoTransferenciaSMM>>(resultadoStr);
            }
            catch { }
            return ls;
        }

        public bool AgregaBultoTransferSMM(int sitioid, int packageid, int layoutid, int IdUsuario, int transferid, string quantity)
        {
            bool ret = false;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?sitioid=" + sitioid + "&packageid=" + packageid + "&layoutid=" + layoutid + "&IdUsuario=" + IdUsuario + "&transferid=" + transferid + "&quantity=" + quantity).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public DataTable bultosCargadosTransferencia(int folioTransferencia)
        {
            DataTable dt = new DataTable();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");


                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?folioTransferencia=" + folioTransferencia).Result;

                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr);
            }
            catch
            {

            }

            return dt;
        }

        public int TraePKID(int SSCCPallet)
        {
            int ret = 0;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?SSCCPallet=" + SSCCPallet).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public bool EliminaPalletDetTrans(int transferid, int packageid)
        {
            bool ret = false;
            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TransferenciaSMM?transferid=" + transferid + "&packageid=" + packageid).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch { }
            return ret;
        }



    }
}
