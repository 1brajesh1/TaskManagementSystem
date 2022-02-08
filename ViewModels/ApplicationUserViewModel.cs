using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.ViewModels
{
    public class ApplicationUserViewModel : IdentityUser
    { 


        public override string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

       

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }



        public string UserRoleId { get; set; }
        public IdentityRole UserRole { get; set; }

        public ICollection<UsersTask> UserTasks { get; set; } = new HashSet<UsersTask>();



    }
}
