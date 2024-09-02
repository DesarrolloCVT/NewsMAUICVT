using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    public class DatosCumplimientoRepoSalaSMM
    {
        public DatosCumplimientoRepoSalaSMM() { }


        public List<SMMReponedoresClass> ListaReponedores()
        {
            List<SMMReponedoresClass> ls = new List<SMMReponedoresClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMCumplRepoSala").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMReponedoresClass>>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                string mns = ex.Message;
            }
            return ls;
        }

        public string insertaRegistroCumplimiento(int idNVerificado, int Verificador, string CodProd, string CodBar, string dispo, string limp, string fefo, string fleje, string fechVenc)
        {
            string ret = "";
            try
            {
                string Fvenc = fechVenc + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/SMMCumplRepoSala?idNVerificado=" + idNVerificado + "&Verificador=" + Verificador + "&CodProd=" + CodProd + "&CodBar=" + CodBar + "&dispo=" + dispo + "&limp=" + limp + "&fefo=" + fefo + "&fleje=" + fleje + "&fechVenc=" + fechVenc).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                                throw new InvalidOperationException();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("insertaRegistroCumplimiento: " + ex.ToString());
            }
            return ret;
        }


    }
}
