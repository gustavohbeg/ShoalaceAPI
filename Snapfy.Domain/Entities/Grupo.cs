using System;
using System.Collections.Generic;
using System.Linq;

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
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                AddNotification("Grupo.Nome", "Nome do grupo é obrigatório");
        }

        public string Nome { get; private set; }
        public string Foto { get; private set; }

        public IReadOnlyCollection<Membro> Membros { get => _membros; }
        public IReadOnlyCollection<Evento> Eventos { get => _eventos; }

        public bool MembroExiste(long usuarioId) => _membros.Any(m => m.UsuarioId == usuarioId);
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
