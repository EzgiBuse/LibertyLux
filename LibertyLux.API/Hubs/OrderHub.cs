using Microsoft.AspNetCore.SignalR;

namespace LibertyLux.API.Hubs
{
    public class OrderHub :Hub
    {
        public async Task NewOrderNotification()
        {
            await Clients.All.SendAsync("ReceiveOrder");
        }
    }
}
