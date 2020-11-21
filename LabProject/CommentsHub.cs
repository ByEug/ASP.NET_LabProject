using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace LabProject
{
    [Authorize]
    public class CommentsHub : Hub
    {
        /*public async Task Send(string message)
        {
            var User = Context.User;

            if (User.Identity.IsAuthenticated)
            {
                string UserName = User.Identity.Name;
                await this.Clients.All.SendAsync("Send", message, UserName);
            }
        }*/
    }
}
