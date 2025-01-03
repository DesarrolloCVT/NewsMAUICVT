﻿using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Datos
{
    internal class DatosApp
    {
        public string TraeVersion()
        {
            string vers = "0.0.0";
            try
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };

                var rest2 = ClientHttp.GetAsync("api/DatosAPP?idAPP=" + 1).Result;
                if (rest2.IsSuccessStatusCode)
                {
                    var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                    vers = JsonConvert.DeserializeObject<string>(resultadoStr) ??
                        throw new InvalidOperationException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Version: " + vers);
            return vers;
        }

        public List<MenuClass> TraeMenu(int idperfil)
        {
            List<MenuClass> ls = new List<MenuClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Usuario?idPerfilUsuario=" + idperfil).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<MenuClass>>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("TraeMenu: " + ex.ToString());
            }
            return ls;
        }
        public bool LogUsabilidad(int submenu, string tipo)
        {
            bool resp = false;
            var IdUsuario = App.Iduser;
            var Fecha = DateTime.Now;
            var Tipo = tipo;
            var SubMenu = submenu;

            try
            {
                HttpClient HttpClient = new HttpClient();
                HttpClient.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = HttpClient.GetAsync("LogUsabilidad?idUsuario=" + IdUsuario + "&submenu=" + SubMenu + "&tipoRegistro=" + Tipo).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("LogUsabilidad: " + ex.ToString());
            }
            return resp;
        }
        public bool InsertaRegistroLog(string entidad, int entidad_id
            , string valorAntiguo, string valorNuevo)
        {
            bool resp = false;
            string Entidad = entidad;
            int Entidad_Id = entidad_id;
            string IdUsuario = App.Iduser.ToString();
            string Valor_Antiguo = valorAntiguo;
            string Valor_Nuevo = valorNuevo;

            try
            {
                HttpClient HttpClient = new HttpClient();
                HttpClient.BaseAddress = new Uri("http://wsintranet2.cvt.local/");
                var rest2 = HttpClient.GetAsync("RegistroLog?Entidad=" + Entidad + "&Entidad_Id=" + Entidad_Id + "&Usuario_Id=" + IdUsuario + "&Valor_Antiguo=" + Valor_Antiguo + "&Valor_Nuevo=" + Valor_Nuevo).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                resp = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("InsertaRegistroLog: " + ex.ToString());
            }
            return resp;
        }
    }
}
