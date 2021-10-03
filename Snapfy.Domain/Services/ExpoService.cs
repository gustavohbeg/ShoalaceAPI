using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expo.Server.Client;
using Expo.Server.Models;

namespace Shoalace.Domain.Services
{
    public static class ExpoService
    {
        public static async void SendNotification(List<string> tokens, string title, string body)
        {
            var expoSDKClient = new PushApiClient();
            var pushTicketReq = new PushTicketRequest()
            {
                PushTo = tokens,
                PushBadgeCount = 7,
                PushTitle = title,
                PushBody = body,
                PushSound = "default"
            };

            var result = await expoSDKClient.PushSendAsync(pushTicketReq);

            if (result?.PushTicketErrors?.Count > 0)
            {
                foreach (var error in result.PushTicketErrors)
                {
                    Console.WriteLine($"Error: {error.ErrorCode} - {error.ErrorMessage}");
                }
            }
        }
    }
}
