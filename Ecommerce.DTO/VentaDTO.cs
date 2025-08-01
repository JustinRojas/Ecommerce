﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public decimal? Total { get; set; }
 
        public ICollection<DetalleVentaDTO> DetallesVenta { get; set; } = new List<DetalleVentaDTO>();
    }
}
