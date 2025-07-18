﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Modelo;
using Ecommerce.DTO;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using AutoMapper;
namespace Ecommerce.Servicio.Implementacion
{
    public class DashBoardServicio: IDashBoardService
    {
        private readonly IGenericoRepositorio<Usuario> _usuarioRepositorio;
         private readonly IGenericoRepositorio<Producto> _productoRepositorio;
         private readonly IVentaRepositorio _ventaRepositorio;

        private readonly IMapper _mapper;

        public DashBoardServicio(IGenericoRepositorio<Usuario> usuarioRepositorio,
            IGenericoRepositorio<Producto> productoRepositorio,
             IVentaRepositorio ventaRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _productoRepositorio = productoRepositorio;
            _ventaRepositorio = ventaRepositorio;
       
        }

        private string Ingresos()
        {
            var consulta = _ventaRepositorio.Get();
            decimal? ingresos = consulta.Sum(x => x.Total);
            return Convert.ToString(ingresos);
        }
        private int Ventas()
        {
            var consulta = _ventaRepositorio.Get();
            int total = consulta.Count();
            return total;
        }
         private int Clientes()
        {
                       
                try
                {
                    var consulta = _usuarioRepositorio.Get(u => u.Rol != null && u.Rol.ToLower() == "cliente");
                    return consulta?.Count() ?? 0;
                }
                catch (Exception ex)
                {
                    // Loggear el error si es necesario
                    Console.WriteLine($"Error al contar clientes: {ex.Message}");
                    return 0;
                }
            
        }
          private int Productos()
        {
            var consulta = _productoRepositorio.Get();
            int total = consulta.Count();
            return total;
        }


        public DashBoardDTO Resumen()
        {
            try
            {
                DashBoardDTO dto = new DashBoardDTO()
                {
                    TotalCliente = Clientes(),
                    TotalIngresos = Ingresos(),
                    TotalVentas = Ventas(),
                    TotalProductos = Productos()
                };

                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
