using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Models
{
    public class UserDataContext : IdentityDbContext<User>
    {
        public UserDataContext(DbContextOptions<UserDataContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

    }
}
