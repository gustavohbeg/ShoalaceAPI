using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comtele.Sdk.Services;

namespace Shoalace.Domain.Services
{
    public static class Comlete
    {
        private static string API_KEY = "0b64ebf4-d310-48da-90be-3de615fc714b";

        public static void SendCode(string number, string code)
        {
            var textMessageService = new TextMessageService(API_KEY);
            var result = textMessageService.Send(
             "",                        // Sender: Id de requisicao da sua aplicacao para ser retornado no relatorio, pode ser passado em branco.
             "Seu código de acesso Shoalace é: " + code,                 // Content: Conteudo da mensagem a ser enviada.
             new string[] { number }  // Receivers: Numero de telefone que vai ser enviado o SMS.
            );

            if (result.Success)
            {
                Console.WriteLine("A mensagem foi enviada com sucesso.");
            }
            else
            {
                Console.WriteLine("A mensagem não pode ser enviada. Detalhes: " + result.Message);
            }
        }
    }
}
