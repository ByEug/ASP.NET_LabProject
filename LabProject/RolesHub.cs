using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace LabProject
{
    public class RolesHub : Hub
    {
        /*public async Task DeleteRole(string message)
        {
            await this.Clients.All.SendAsync("ShowMessage", message);
        }*/
    }
}
