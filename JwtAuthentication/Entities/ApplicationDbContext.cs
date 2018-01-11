using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Wishlist> wishDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Wishlist>().ToTable("wishDetails");
        }
        private static string GetConnectionString()
        {
            const string databaseName = "webapijwt";
            const string databaseUser = "sa";
            const string databasePass = "Password1";

            return $"Server=TCGDLAP121;" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling=true;";
        }
    }
}
