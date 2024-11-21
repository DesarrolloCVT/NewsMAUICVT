using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsMauiCVT.Model
{
    internal class PedidosAsignacion
    {
        public int Order_ID { get; set; }
        public string ItemCode { get; set; }
        public string Lote { get; set; }
        public int Cantidad { get; set; }
    }
}
