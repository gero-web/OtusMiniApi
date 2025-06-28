using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Infrastructors.Contexts
{
    class UsersDbCOntext(ILogger<UsersDbCOntext> logger, 
                         IConfiguration configuration) : DbContext
    {
        private ILogger<UsersDbCOntext> _logger = logger;
        private readonly IConfiguration configuration = configuration;

        public DbSet<User> Users { get; set; }

         

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = Environment.GetEnvironmentVariable("HOST_DB");
            var port = Environment.GetEnvironmentVariable("PORT_DB");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("USER_DB");
            var pass = Environment.GetEnvironmentVariable("PASS_DB");
            var connectionString = "";

            if (Environment.GetEnvironmentVariable("ASPNETCORESTAGE") == "Development")
            {
                connectionString = configuration.GetConnectionString("defaultDb");
            }
            else
            {
              connectionString =  $"Host={host};Port={port};Database={dbName};Username={user};Password={pass}";
            }

            optionsBuilder.UseNpgsql(connectionString);

            
            
            
            base.OnConfiguring(optionsBuilder);
        }

    }
}
