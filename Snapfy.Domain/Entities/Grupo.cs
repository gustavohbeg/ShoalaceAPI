using System;
using System.Collections.Generic;

namespace Shoalace.Domain.Entities
{
    public class Grupo : Base
    {
        private readonly List<Membro> _membros;
        private readonly List<Evento> _eventos;
        public Grupo(string nome, string foto) : base()
        {
            _membros = new List<Membro>();
            _eventos = new List<Evento>();
            PreencherGrupo(nome, foto);
        }


        public void PreencherGrupo(string nome, string foto)
        {
            Alterado = DateTime.Now;
            Nome = nome;
            Foto = foto;

            if (string.IsNullOrEmpty(Nome))
                AddNotification("Grupo.Nome", "Nome do grupo é obrigatório");
        }

        public string Nome { get; private set; }
        public string Foto { get; private set; }

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
