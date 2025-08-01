﻿using Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Servicio.Contrato
{
    public interface ICategoriaServicio
    {
        Task<List<CategoriaDTO>> Lista( string buscar);

        Task<CategoriaDTO> Obtener(int id);

        Task<CategoriaDTO> Crear(CategoriaDTO modelo);
        Task<bool> Editar(CategoriaDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
