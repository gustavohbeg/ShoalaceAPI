using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Infra.Contexto;
using System;
using System.Threading.Tasks;

namespace Shoalace.Infra.Repositories
{
    public class ErroRepository : BaseRepository<Erro>, IErroRepository
    {
        public ErroRepository(ShoalaceContexto DigibyteContexto) : base(DigibyteContexto) { }

        public async Task<long> TratamentoException(Exception exception, string parametros)
        {
            Erro erro = new Erro(0, DateTime.Now, parametros, exception.Message, exception.StackTrace);
            await Adicionar(erro);
            await Commit();
            return erro.Id;
        }
    }
}
