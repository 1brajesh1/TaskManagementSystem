using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models

{
    public class ApplicationUser : IdentityUser
    {


        public override string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        //public string Avatar { get; set; }

        //[Required(ErrorMessage = "Please choose profile image")]
        //public string ImagePath { get; set; }

        //[Required(ErrorMessage = "Please choose profile image")]
        //public string ProfilePicture { get; set; }

        //public string ProfileImage { get; set; }

        //public byte[] ProfilePicture { get; set; }

        public byte[] ProfilePicture { get; set; }



        public string UserRoleId { get; set; }
        public IdentityRole UserRole { get; set; }

        public ICollection<UsersTask> UserTasks { get; set; } = new HashSet<UsersTask>();

        //public bool Selected { get; internal set; }
    }

}

//    public class ApplicationUser : IdentityUser
//    {
//        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

//        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

//        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }

//        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
//        public ICollection<UsersTask> UserVsTasks { get; set; }

//    }

//    public class ApplicationRole : IdentityRole
//    {
//        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
//    }

//    public class ApplicationUserRole : IdentityUserRole<string>
//    {
//        public virtual ApplicationUser User { get; set; }

//        public virtual ApplicationRole Role { get; set; }
//    }
//}

//public ViewTask ViewTask { get; set; }
//public List<UsersTask> UserVsTasks { get; set; }