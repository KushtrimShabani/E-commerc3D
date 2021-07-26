using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace E_commerc3D.Models
{
    public class Chat : Hub
    {
        public async Task Send( Message message)
        {
          
          await  Clients.All.SendAsync("RecediveMessage", message);
        }
    }
}

