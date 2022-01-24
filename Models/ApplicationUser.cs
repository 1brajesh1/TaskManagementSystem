﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<UserVsTask> UserVsTasks { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}

//public ViewTask ViewTask { get; set; }
//public List<UserVsTask> UserVsTasks { get; set; }