using Flunt.Notifications;
using Shoalace.Domain.Interfaces.Commands;

namespace Shoalace.Domain.Commands
{
    public class ResultadoCommand : Notifiable, IResultadoCommand
    {
        public ResultadoCommand()
        {
            PreencherRetorno(null);
        }

        public ResultadoCommand(object retorno)
        {
            PreencherRetorno(retorno);
        }

        public object Retorno { get; private set; }
        public bool Valido() => Valid;
        public bool Invalido() => Invalid;

        public void PreencherRetorno(object retorno)
        {
            Retorno = retorno;
        }
    }
}
