﻿using ApiFindHome.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Data
{
    public class ApplicationDbContext : DbContext
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

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<AdFor> AdFors { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-11LDN42\\SQLEXPRESS;Database=FindHome;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
