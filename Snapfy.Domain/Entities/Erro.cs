using System;

namespace Shoalace.Domain.Entities
{
    public class Erro
    {
        public Erro(int id, DateTime dataHora, string parametros, string message, string stackTrace)
        {
            Id = id;
            DataHora = dataHora;
            Parametros = parametros;
            Message = message;
            StackTrace = stackTrace;
        }

        public int Id { get; private set; }
        public DateTime DataHora { get; private set; }
        public string Parametros { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
    }
}
