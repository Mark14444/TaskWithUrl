using Inforce.Domain;
using Inforce.Domain.Entities;
using Inforce.Repository.TableConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inforce.Repository
{
    public class InforceContext:DbContext
    {
        public InforceContext(DbContextOptions options)
            :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
                string connectionString = builder.Build().GetConnectionString("conn");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UrlConfiguration())
                        .ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "1488@gmail.com",
                    Password = "1488",
                    Role = Role.Admin
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
