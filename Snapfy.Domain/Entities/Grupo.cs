using Flunt.Notifications;
using Flunt.Validations;
using Shoalace.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shoalace.Domain.Entities
{
    public class Grupo : Base
    {
        private readonly List<Membro> _membros;
        private readonly List<Evento> _eventos;
        private readonly List<Mensagem> _mensagens;
        public Grupo(string nome, string foto) : base()
        {
            _membros = new();
            _eventos = new();
            _mensagens = new();
            PreencherGrupo(nome, foto);
        }

        public string Nome { get; private set; }
        public string Foto { get; private set; }

        public IReadOnlyCollection<Membro> Membros { get => _membros; }
        public IReadOnlyCollection<Evento> Eventos { get => _eventos; }
        public IReadOnlyCollection<Mensagem> Mensagens { get => _mensagens; }

        public bool MembroExiste(long usuarioId) => _membros.Any(m => m.UsuarioId == usuarioId);

        public void PreencherGrupo(string nome, string foto)
        {
            Alterado = DateTime.Now;
            Nome = nome;
            Foto = foto;
            Validate();
        }

        public void Validate() =>
            AddNotifications(new Contract<Notification>[]
            {
                GrupoValidation.ValidateNome(Nome)
            });

        public void AdicionarMembro(Membro membro)
        {
            if (!MembroExiste(membro.UsuarioId))
                _membros.Add(membro);
        }

        public void RemoverMembro(Membro membro)
        {
            if (MembroExiste(membro.UsuarioId))
                _membros.Remove(membro);
        }
    }
}
