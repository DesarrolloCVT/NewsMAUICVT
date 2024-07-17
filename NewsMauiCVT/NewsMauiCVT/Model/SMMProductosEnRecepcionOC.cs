using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    public class SMMProductosEnRecepcionOC
    {
        public string CodProducto { get; set; }
        public string Descripcion { get; set; }
        public int NOrden { get; set; }
        public float CantOrden { get; set; }
        public int CodUm { get; set; }
        public string NombreUm { get; set; }
        public string CodProveedor { get; set; }
        public string Proveedor { get; set; }
        public string CodBarra { get; set; }
        public DateTime Fecha { get; set; }
        public int Precio { get; set; }

    }
}
