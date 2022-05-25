using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamIK.API.Controllers
{
    public class MyHub:Hub
    {

        public static List<string> Message { get; set; } = new List<string>();

        public async Task SendMessage(string name)
        {
            Message.Add(name);
            await Clients.All.SendAsync("Receive", name);
        }
        public async Task GetMessage()
        {
            await Clients.All.SendAsync("ReceiveNames", Message);
        }
    }
}
