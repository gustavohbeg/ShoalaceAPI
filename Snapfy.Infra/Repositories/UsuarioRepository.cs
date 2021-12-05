using Microsoft.EntityFrameworkCore;
using Shoalace.Domain.Entities;
using Shoalace.Domain.Interfaces.Repositories;
using Shoalace.Domain.Queries;
using Shoalace.Domain.Responses;
using Shoalace.Infra.Contexto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoalace.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ShoalaceContexto ShoalaceContexto) : base(ShoalaceContexto) { }

        public async Task<Usuario> ObterPorNumero(string numero) =>
            await _ShoalaceContexto.Usuario.Where(UsuarioQuery.ObterPorNumero(numero)).FirstOrDefaultAsync();

        public new async Task<Usuario> ObterPorId(long id) =>
            await _ShoalaceContexto.Usuario.Where(UsuarioQuery.ObterPorId(id)).FirstOrDefaultAsync();

        public async Task<ContatoChatResponse> ObterContatoChatPorId(long id) =>
            await _ShoalaceContexto.Usuario.Where(UsuarioQuery.ObterPorId(id))
            .Select(u => new ContatoChatResponse()
            {
                Id = u.Id,
                Nome = u.Nome,
                Foto = u.Foto,
                Bio = u.Bio,
                Aniversario = u.Aniversario,
                Sexo = u.Sexo,
                Numero = u.Numero,
                IsGrupo = false,
                Cadastro = u.Cadastro,
            }).FirstOrDefaultAsync();

        public async Task<List<Usuario>> ObterContatos(long id) =>
            await _ShoalaceContexto.Usuario.Where(u => _ShoalaceContexto.Contato.Any(c => c.UsuarioId == id && c.UsuarioContatoId == u.Id)).AsNoTracking().ToListAsync();
    }
}
