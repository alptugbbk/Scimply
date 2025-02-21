using DbScimplyAPI.Domain.Entities.Admin;
using DbScimplyAPI.Domain.Entities.Log;
using DbScimplyAPI.Domain.Entities.Role;
using DbScimplyAPI.Domain.Entities.User;
using DbScimplyAPI.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Context
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Schema> Schemas { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Logs> Logs { get; set; }



        // configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SchemaConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new LoggerConfiguration());
        }


    }
}
