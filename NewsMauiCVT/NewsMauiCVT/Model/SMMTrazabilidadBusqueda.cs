using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class SMMTrazabilidadBusqueda
    {
        public int Site_Id { get; set; }
        public string Site_Description { get; set; }
        public int Layout_Id { get; set; }
        public int Package_Id { get; set; }
        public string CodProducto { get; set; }
        public string Producto { get; set; }
        public int NRecepcion { get; set; }
        public DateTime FRecepcion { get; set; }
        public string Proveedor { get; set; }
        public string NPallet { get; set; }
        public float Cantidad { get; set; }
        public int Estado { get; set; }
        public string Lote { get; set; }
        public string CodBarr { get; set; }
        public float CantReserva { get; set; }
        public string Ubicacion { get; set; }
        public DateTime Package_ProductionDate { get; set; }
        public DateTime Package_ExpiresDate { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public float CantInicial { get; set; }
        public DateTime Package_InDate { get; set; }
        public DateTime Package_OutDate { get; set; }


    }
}
