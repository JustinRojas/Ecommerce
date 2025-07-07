using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;

namespace Ecommerce.Repositorio.Implementacion
{
    public class VentaRepositorio : GenericoRepositorio<Venta>, IVentaRepositorio
    {

        private readonly DbEcommerceContext _dbContext;
        public VentaRepositorio(DbEcommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach(DetalleVenta dv in modelo.DetallesVenta)
                    {
                        Producto producto_encontrado = _dbContext.Producto.Where(p => p.IdProducto == dv.IdProducto).First();
                        //Dismunuimos el sctock
                        producto_encontrado.Cantidad = producto_encontrado.Cantidad-dv.Cantidad;

                        _dbContext.Producto.Update(producto_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();


                    await _dbContext.Ventas.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();
                    ventaGenerada = modelo;
                    transaction.Commit();


                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                    throw;
                }
                return ventaGenerada;
            }
        }
    }
}

