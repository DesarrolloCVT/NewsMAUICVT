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
        public bool InsertaCheckListGrua(Dictionary<string,string> checkList)
        {
            bool resp = false;
            try
            {
                string username = App.UserSistema;
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("api/CheckListGruas/InsertaCheckListGrua?Usuario_Responsable=" + App.NombreUsuario
                    +"&Numero_Grua=" + checkList.GetValueOrDefault("NumeroGrua") 
                    +"&Area_Trabajo="+ checkList.GetValueOrDefault("AreaTrabajo") + "&Tipo_Maquina="+ checkList.GetValueOrDefault("TipoMaquinaria")
                    +"&Turno=1&Horometro="+ checkList.GetValueOrDefault("Horometro") + "&Fecha="+ checkList.GetValueOrDefault("Fecha") 
                    +"&Estado_Luces="+ checkList.GetValueOrDefault("Luces") + "&Estado_Motor="+ checkList.GetValueOrDefault("Motor") 
                    +"&Fuga_Agua_Aceite="+ checkList.GetValueOrDefault("Fugas") + "&Estado_Direccion="+ checkList.GetValueOrDefault("Direccion")
                    +"&Estado_Transmision="+ checkList.GetValueOrDefault("Transmision") + "&Escalera_Acceso_Pasamanos="+ checkList.GetValueOrDefault("Escalera") 
                    +"&Estado_Bocina="+ checkList.GetValueOrDefault("Bocina") + "&Alarma_Retroceso="+ checkList.GetValueOrDefault("Alarma") 
                    +"&Espejo_Retrovisor="+ checkList.GetValueOrDefault("Espejos") + "&Estado_Tablero_Datos="+ checkList.GetValueOrDefault("Tablero")
                    +"&Estado_Extintor="+ checkList.GetValueOrDefault("Extintor") + "&Estado_Bateria="+ checkList.GetValueOrDefault("Bateria") 
                    +"&Estado_Asiento="+ checkList.GetValueOrDefault("Asiento") + "&Cinturon_Seguridad="+ checkList.GetValueOrDefault("Cinturon") 
                    +"&Baliza_Pertiga="+ checkList.GetValueOrDefault("Baliza") + "&Estado_Neumaticos="+ checkList.GetValueOrDefault("Neumaticos")
                    +"&Llantas_Tuercas="+ checkList.GetValueOrDefault("Llantas") + "&Cadenas_Torre="+ checkList.GetValueOrDefault("Cadenas") 
                    +"&Unas_Horquilla="+ checkList.GetValueOrDefault("Unashorquilla") + "&Soporte_Cilindro="+ checkList.GetValueOrDefault("Soportecilindro") 
                    +"&Flexible_Polea_Rodamiento="+ checkList.GetValueOrDefault("Flexible") + "&Seguro_Una_Horquilla="+ checkList.GetValueOrDefault("Segurohorquilla")
                    +"&Punto_Bloqueo="+ checkList.GetValueOrDefault("Puntodebloqueo") + "&Observaciones="+ checkList.GetValueOrDefault("Observaciones")).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
                if (rest.IsSuccessStatusCode)
                {
                    resp = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertaCheckListGrua: " + ex.Message);
            }
            return resp;
        }
    }
}
