﻿using Ecommerce.DTO;
namespace Ecommerce.WebAssembly.Servicios.Contrato
{
    public interface IDashboardServicio
    {
       

        Task<ResponseDTO<DashBoardDTO>> Resumen();
    }
}
