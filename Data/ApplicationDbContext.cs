using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext<
        ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>

        
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });



            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("MyUsers");
            });
            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("MyRoles");
            });
            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("MyUserRoles");
            });

            //      modelBuilder.Entity<UsersTask>()
            //.HasKey(bc => new { bc.TaskId, bc.ApplicationUserId });

            //      modelBuilder.Entity<UsersTask>()
            //          .HasOne(bc => bc.Tasks)
            //          .WithMany(b => b.UserTasks)
            //          .HasForeignKey(bc => bc.TaskId);

            //      modelBuilder.Entity<UsersTask>()
            //          .HasOne(bc => bc.ApplicationUser)
            //          .WithMany(c => c.UserTasks)
            //          .HasForeignKey(bc => bc.ApplicationUserId);
        







    }
        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskCategory> TaskCategories { get; set; }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //public DbSet<IdentityRole> RoleManager { get; set; }

        public DbSet<UserVsTask> UserVsTasks { get; set; }

    }
    }

    

        
