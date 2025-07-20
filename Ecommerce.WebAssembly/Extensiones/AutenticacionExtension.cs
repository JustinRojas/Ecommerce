using Blazored.LocalStorage;
using Ecommerce.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.WebAssembly.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    { 
        private readonly ILocalStorageService _localStorageService;

        //Se crea un claims para guardar la informacion, en este caso vació para luego resetear los campos
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsPrincipal());

        public AutenticacionExtension(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }


        //Método para guardar la información del usuario
        //Si acá pasamos null es lo que indica que se debe cerrar la sesión
        public async Task ActualizarEstadoAutenticacion(SesionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;
            if(sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                       new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                       new Claim(ClaimTypes.Name, sesionUsuario.NombreCompleto.ToString()),
                       new Claim(ClaimTypes.Email, sesionUsuario.Correo.ToString()),
                       new Claim(ClaimTypes.Role, sesionUsuario.Rol.ToString()),
                   
                       //El texto entre comillas hace referencia al tipo de autenticación
                       //que va a tener
                },"JwtAuth"));


                //Se procede a guadar la información en el localStorage
                await _localStorageService.SetItemAsync("sesionUsuario", sesionUsuario);

            }
            else
            {
                //Si la sesion es nula, se iguala al claim sin información creado antes
                claimsPrincipal = _sinInformacion;
                // y se borra lo que esta en el local storage con nombre de sesionUsuario
                await _localStorageService.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult( new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _localStorageService.GetItemAsync<SesionDTO>("sesionUsuario");

            if (sesionUsuario == null)
            {
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            }
            else
            {
               var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                       new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                       new Claim(ClaimTypes.Name, sesionUsuario.NombreCompleto.ToString()),
                       new Claim(ClaimTypes.Email, sesionUsuario.Correo.ToString()),
                       new Claim(ClaimTypes.Role, sesionUsuario.Rol.ToString()),
                   
                       //El texto entre comillas hace referencia al tipo de autenticación
                       //que va a tener
                }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            
            
        }
    }
}
