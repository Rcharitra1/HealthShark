using HealthShark.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthShark.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<BodyType> BodyTypes { get; set; }


        public DbSet<UserPlan> UserPlans { get; set; }


        public DbSet<ApplicationUser> ApplicationUsers {get; set;}


    }
}
