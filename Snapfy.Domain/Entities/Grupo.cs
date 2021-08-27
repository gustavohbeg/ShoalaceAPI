using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Entities
{
    public class Grupo : Base
    {
        private readonly List<Membro> _membros;
        private readonly List<Evento> _eventos;
        public Grupo(string nome, byte? foto) : base()
        {
            Nome = nome;
            Foto = foto;
            _membros = new List<Membro>();
            _eventos = new List<Evento>();
        }

        public void PreencherGrupo(string nome, byte? foto)
        {
            Alterado = DateTime.Now;
            Nome = nome;
            Foto = foto;
        }

        public string Nome { get; private set; }
        public byte? Foto { get; private set; }

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
