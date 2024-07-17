using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class CVT_ControlHigieneClass
    {
        public int Id_ControlHigene { get; set; }
        public DateTime Fecha { get; set; }
        public int Id_Monitor { get; set; }
        public int Id_Area { get; set; }
        public string Tipo_Contrato { get; set; }
        public int Id_Persona { get; set; }
        public int Limpieza_Uniforme { get; set; }
        public int Afeitado_PeloCorto { get; set; }
        public int Uñas { get; set; }
        public int Joyas { get; set; }
        public int Higene { get; set; }
        public string Accion_Correctiva { get; set; }
        public int Promedio { get; set; }
        public int Porcentaje_Cumplimiento { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        public int Estado_Uniforme { get; set; }
        public int Herida_Expuesta { get; set; }
    }
}
