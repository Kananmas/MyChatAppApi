using ChatService.ConnectionMapping;
using Microsoft.AspNetCore.SignalR;
using MyChatAppApi.Models;
using System.Threading.Tasks;

namespace MyChatAppApi.MainHub
{

    public class ChatHub : Hub
    {
        public int clientConntected = 0;
        private readonly static BaseConnectionMapping<string> _connections = new BaseConnectionMapping<string>();

        public async Task GetAllClients()
        {
        }

        public async Task SendMessageToAll(string text)
        {

        }

        public async Task SendMessageToTarget(Message message)
        {


        }

        public async Task RemoveUser(string id)
        {

        }

        public async Task AddUserToDataBase(string username)
        {

        }

        //public override Task OnConnectedAsync()
        //{

        //}

        //public override Task OnDisconnectedAsync(Exception exception)
        //{

        //}
    }
}
