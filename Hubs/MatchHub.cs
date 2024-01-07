using Microsoft.AspNetCore.SignalR;
namespace SCOREgrp05.Hubs
{
    public class MatchHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("NewMatch");
        }
    }
}