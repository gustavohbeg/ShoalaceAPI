using Shoalace.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Repositories
{
    public interface IErroRepository : IBaseRepository<Erro>
    {
        Task<long> TratamentoException(Exception exception, string parametros);
    }
}
