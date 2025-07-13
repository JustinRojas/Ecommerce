using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;
using System.Reflection;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {
        private ILocalStorageService _localStorageService;
        private ISyncLocalStorageService _syncLocalStorageService;
        private IToastService _toastService;

        public CarritoServicio(ILocalStorageService localStorageService,
                               ISyncLocalStorageService syncLocalStorageService,
                               IToastService toastService)
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;
        }

        public event Action? MostrarItems;



        public async Task AgregarCarrito(CarritoDTO modelo)
        {
            try
            {
                //Trae toda la lista de los productos si hay
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                //Esto es en caso de que no hayan productos o no exista el carrito
                if (carrito == null)
                    carrito = new List<CarritoDTO>();

                //valida en caso de que el producto ya este en la lista, para si es así borrar y actualizar
                var encontrado = carrito.FirstOrDefault(c => c.Producto?.IdProducto == modelo.Producto.IdProducto);

                //Verificamos y borramos si fuese que ya existe 
                if (encontrado != null)
                    carrito.Remove(encontrado);

                carrito.Add(modelo);
                await _localStorageService.SetItemAsync("carrito", carrito);

                if (encontrado != null)
                {
                    _toastService.ShowSuccess("Producto fue actualizado en carrito");
                }
                else
                {
                    _toastService.ShowSuccess("Producto fue agregado al carrito");
                }

                MostrarItems.Invoke();
            }
            catch (Exception)
            {

                _toastService.ShowError("No se pudo agregar al carrito");
            }
        }

        public int CantidadProductos()
        {
            // Obtiene el carrito del almacenamiento local de forma síncrona
            var carrito = _syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");

            // Retorna 0 si el carrito es null, de lo contrario retorna la cantidad de productos
            return carrito == null ? 0 : carrito.Count();
        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            // Obtiene el carrito del almacenamiento local de forma asíncrona
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");

            // Si el carrito no existe, crea una nueva lista vacía
            if (carrito == null)
            {
                carrito = new List<CarritoDTO>();
            }

            // Retorna el carrito (puede ser la nueva lista vacía)
            return carrito;
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                // Obtiene el carrito del almacenamiento local
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");

                // Verifica si el carrito existe
                if (carrito != null)
                {
                    // Busca el producto específico por su ID
                    var elemento = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);

                    // Si encontró el producto en el carrito
                    if (elemento != null)
                    {
                        // Elimina el producto de la lista
                        carrito.Remove(elemento);

                        // Guarda el carrito actualizado en el almacenamiento local
                        await _localStorageService.SetItemAsync("carrito", carrito);

                        // Notifica a los suscriptores para actualizar la UI
                        MostrarItems.Invoke();
                    }
                }
            }
            catch (Exception)
            {
                // Relanza cualquier excepción ocurrida
                throw;
            }
        }

        public async Task LimpiarCarrito()
        {
            // Elimina completamente el carrito del almacenamiento local
            await _localStorageService.RemoveItemAsync("carrito");

            // Notifica a los suscriptores para actualizar la UI
            MostrarItems.Invoke();
        }
    }
}
