using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositorio.Contrato
{
    public interface IGenericoRepositorio<TModelo> where TModelo : class
    {
       IQueryable<TModelo> Get(Expression<Func<TModelo, bool>>? filtro = null);

        Task<TModelo> Create(TModelo modelo);
        Task<bool> Edit(TModelo modelo);
        Task<bool> Delete(TModelo modelo);

    }
}
