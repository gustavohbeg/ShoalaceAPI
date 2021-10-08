using Flunt.Notifications;
using Shoalace.Domain.Interfaces.Commands;

namespace Shoalace.Domain.Commands
{
    public class ResultadoCommand : Notifiable<Notification>, IResultadoCommand
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
        public bool Valid { get => IsValid; }
        public bool Invalid { get => !IsValid; }

        public void PreencherRetorno(object retorno) => Retorno = retorno;
    }
}
