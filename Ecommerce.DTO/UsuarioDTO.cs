﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class UsuarioDTO
    {

        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Ingrese nombre completo")]
        public string? NombreCompleto { get; set; }
        [Required(ErrorMessage = "Ingrese correo")]
        public string? Correo { get; set; }
        [Required(ErrorMessage = "Ingrese contraseña")]
        public string? Clave { get; set; }
        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        public string? ConfirmarClave { get; set; }

        public string? Rol { get; set; }


    }
}
