using HealthShark.Models.Models;
using HealthShark.Models.Models.ViewModels;
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


        public DbSet<WorkOutType> WorkOutTypes { get; set; }

        public DbSet<Diet> Diets { get; set; }


        public DbSet<UserVM> UserVMs { get; set; }

        public DbSet<UserAssignmentVM> UserAssignmentVMs { get; set; }



    }
}
