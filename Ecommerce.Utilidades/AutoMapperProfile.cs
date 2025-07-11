﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Modelo;


namespace Ecommerce.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<Usuario, SesionDTO>();
            CreateMap<UsuarioDTO,Usuario>();

            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>();


            CreateMap<Producto, ProductoDTO>();
            //Le decimos que a la hora de convertir de ProductoDTO a Producto ignore la propiedad Categoria
            CreateMap<ProductoDTO, Producto>().ForMember(destino =>
            destino.Categoria, opt => opt.Ignore()
            );

            CreateMap<DetalleVenta, DetalleVentaDTO>();
            CreateMap<DetalleVentaDTO, DetalleVenta>();

            CreateMap<Venta, VentaDTO>();
            CreateMap<VentaDTO, Venta>();


        }
    }
}
