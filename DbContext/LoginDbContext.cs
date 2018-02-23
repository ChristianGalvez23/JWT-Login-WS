using System;
using login_webapi.DbContext.EntityConfiguration;
using login_webapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace login_webapi.DbContext {
    public class LoginDbContext : Microsoft.EntityFrameworkCore.DbContext {
        public DbSet<User> Users { get; set; }

        public LoginDbContext (DbContextOptions options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration<User> (new UserConfiguration ());
        }

    }
}