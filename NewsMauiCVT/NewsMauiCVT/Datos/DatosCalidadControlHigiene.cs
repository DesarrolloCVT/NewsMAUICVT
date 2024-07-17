using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Datos
{
    public class DatosCalidadControlHigiene
    {

        public DatosCalidadControlHigiene() { }


        public List<PersonalClass> ListaPerdonal()
        {
            List<PersonalClass> ls = new List<PersonalClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ControlHigiene/listaMonitor").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<PersonalClass>>(resultadoStr);
            }
            catch (Exception ex)
            {
                string mns = ex.Message;
            }
            return ls;
        }
        public List<PersonalClass> ListaPerdonalFull()
        {
            int rs = 1;
            List<PersonalClass> ls = new List<PersonalClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ControlHigiene/listaPersonal").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<PersonalClass>>(resultadoStr);
            }
            catch (Exception ex)
            {
                string mns = ex.Message;
            }
            return ls;
        }

        public List<AreaClass> ListaAreas()
        {
            int rs = 1;
            List<AreaClass> ls = new List<AreaClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/ControlHigiene/listaArea").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<AreaClass>>(resultadoStr);
            }
            catch (Exception ex)
            {
                string mns = ex.Message;
            }
            return ls;
        }

        public string CreaDatosControlHigiene(CVT_ControlHigieneClass c)

        {
            string res = "-1";



            string url = "http://wsintranet.cvt.local/api/ControlHigiene/PostControl";
            WebRequest oRequest = WebRequest.Create(url);
            oRequest.ContentType = "application/json; charset=utf-8";
            oRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(oRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(c);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)oRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    res = result.ToString();

                    //JsonConvert.DeserializeObject<string>(result);
                }
                return res;

            }
        }
    }
}
