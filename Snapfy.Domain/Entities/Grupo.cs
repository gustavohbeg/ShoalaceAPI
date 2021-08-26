using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Entities
{
    public class Grupo : Base
    {
        private readonly List<Membro> _membros;
        private readonly List<Evento> _eventos;
        public Grupo(string nome, byte? foto, long usuarioId) : base()
        {
            Nome = nome;
            Foto = foto;
            UsuarioId = usuarioId;
            _membros = new List<Membro>();
            _eventos = new List<Evento>();
        }

        public void PreencherGrupo(string nome, byte? foto, long usuarioId)
        {
            Alterado = DateTime.Now;
            Nome = nome;
            Foto = foto;
            UsuarioId = usuarioId;
        }

        public string Nome { get; private set; }
        public byte? Foto { get; private set; }
        public long UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        public IReadOnlyCollection<Membro> Membros { get => _membros; }
        public IReadOnlyCollection<Evento> Eventos { get => _eventos; }

        public void AdicionarMembro(Membro membro)
        {
            if (!_membros.Contains(membro))
                _membros.Add(membro);
        }

        public void RemoverMembro(Membro membro)
        {
            if (_membros.Contains(membro))
                _membros.Remove(membro);
        }
    }
}
