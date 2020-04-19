using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teamcast.Models;

namespace Teamcast.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventMember> EventMember { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
    }
}
