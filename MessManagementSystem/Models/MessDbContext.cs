using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MessManagementSystem.Models
{
    public class MessDbContext:DbContext
    {
        public MessDbContext(): base("MessManagement")
        {

        }

        public DbSet<Member> Members { set; get; }

        public DbSet<Bazar> Bazars { get; set; }
        public DbSet<Meal> Meals { set; get; } 

    }
}