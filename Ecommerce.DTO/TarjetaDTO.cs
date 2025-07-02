using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class TarjetaDTO
    {
        [Required(ErrorMessage = "Ingrese titular")]
        public string? Titular { get; set; }
        [Required(ErrorMessage = "Ingrese número de tarjeta")]
        public string? Numero { get; set; }
        [Required(ErrorMessage = "Ingrese vigencia de tarjeta")]
        public string? Vigencia { get; set; }
        [Required(ErrorMessage = "Ingrese código de seguridad")]
        public string? CVV { get; set; }

    }
}
