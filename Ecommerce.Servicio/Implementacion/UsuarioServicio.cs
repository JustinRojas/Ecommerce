﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Ecommerce.Modelo;
using Ecommerce.DTO;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using AutoMapper;


namespace Ecommerce.Servicio.Implementacion
{
    public class UsuarioServicio: IUsuarioServicio
    {
        private readonly IGenericoRepositorio<Usuario> _modeloRepositorio;
        private readonly IMapper _mapper;

        public UsuarioServicio(IGenericoRepositorio<Usuario> modeloRepositorio, IMapper mapper)
        {
            _modeloRepositorio = modeloRepositorio;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                //Convertimos de UsuarioDTO a usuario
                var dbModelo = _mapper.Map<Usuario>(modelo);
                var rspModelo = await _modeloRepositorio.Create(dbModelo);

                if (rspModelo.IdUsuario != 0) 
                { 
                    return _mapper.Map<UsuarioDTO>(rspModelo);
                }else
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Get(p => p.IdUsuario == modelo.IdUsuario);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if(fromDbModelo != null)
                {
                    fromDbModelo.NombreCompleto = modelo.NombreCompleto;
                    fromDbModelo.Correo = modelo.Correo;
                    fromDbModelo.Clave = modelo.Clave;

                    var respuesta = await _modeloRepositorio.Edit(fromDbModelo);

                    if(!respuesta)
                        throw new TaskCanceledException("No se pudo editar");
                    return respuesta;
                }
                else
                {
                    throw new TaskCanceledException("No se encontaron resultados");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Get(p => p.IdUsuario == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    var respuesta = await _modeloRepositorio.Delete(fromDbModelo);
                    if(!respuesta)                   
                        throw new TaskCanceledException("No se pudo eliminar");

                    return respuesta;

                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                } 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SesionDTO> GetAutorizacion(LoginDTO modelo)
        {
            try
            {
                //Aca se crea la consulta que luego se ejecuta en la siguiente línea
                var consulta = _modeloRepositorio.Get(p => p.Correo == modelo.Correo && p.Clave == modelo.Clave);
                //Se ejecuta la linea la cual busca el modelo en la bd
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                {
                    return _mapper.Map<SesionDTO>(fromDbModelo);
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron coicidencias");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioDTO> GetUsuario(int id)
        {
            try
            {
                var consulta = _modeloRepositorio.Get(p => p.IdUsuario == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null)
                    return _mapper.Map<UsuarioDTO>(fromDbModelo);
                else
                    throw new TaskCanceledException("No se encontraron coicidencias");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<UsuarioDTO>> Lista(string rol, string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Get(p => p.Rol == rol && string.Concat(p.NombreCompleto.ToLower(), p.Correo.ToLower()).Contains(buscar.ToLower()));
                List<UsuarioDTO> lista = _mapper.Map<List<UsuarioDTO>>(await consulta.ToListAsync());
                return lista;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
