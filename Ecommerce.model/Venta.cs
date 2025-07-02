using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Modelo
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
