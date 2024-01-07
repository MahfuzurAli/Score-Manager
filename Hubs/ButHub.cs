using Microsoft.AspNetCore.SignalR;
namespace SCOREgrp05.Hubs
{
    public class ButHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("NewBut");
        }
    }
}