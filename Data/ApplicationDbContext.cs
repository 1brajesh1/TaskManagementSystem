using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>

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
                b.ToTable("MyUsers");
            });

            modelBuilder.Entity<UsersTask>()
             .HasKey(bc => new { bc.TaskId, bc.ApplicationUserId });



            modelBuilder.Entity<UsersTask>()
                .HasOne(bc => bc.Task)
                .WithMany(b => b.UserTasks)
                .HasForeignKey(bc => bc.TaskId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<UsersTask>()
             .HasOne(bc => bc.ApplicationUser)
             .WithMany(c => c.UserTasks)
             .HasForeignKey(bc => bc.ApplicationUserId)
              .OnDelete(DeleteBehavior.ClientCascade);



            this.Roles(modelBuilder);

            this.Users(modelBuilder);

            this.UserRoles(modelBuilder);

        }


        private new void Users(ModelBuilder modelBuilder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                NormalizedUserName = "Admin".ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
               
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            //PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            //passwordHasher.HashPassword(user, "Admin@123");

            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }

        private new void Roles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "2", Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "Manager" },
                new IdentityRole() { Id = "3", Name = "Employee", ConcurrencyStamp = "3", NormalizedName = "Employee" }
                );
        }

        private new void UserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "1", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }


        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskCategory> TaskCategories { get; set; }

        public DbSet<UsersTask> UsersTasks { get; set; }

    }
}




