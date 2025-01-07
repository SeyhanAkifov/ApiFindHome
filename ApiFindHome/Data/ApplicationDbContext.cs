using ApiFindHome.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       

        public ApplicationDbContext()
        {
           
        }

        

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        


        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<AdFor> AdFors { get; set; }

        public DbSet<UserLike> UserLikes { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Conversation> Conversations { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
