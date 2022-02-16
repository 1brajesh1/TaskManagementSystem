﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementSystem.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[]? ProfilePicture { get; set; }


        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var profilePicture = user.ProfilePicture;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ProfilePicture = profilePicture
            };

            

        }

        public async Task<IActionResult> OnGetAsync(ApplicationUser user1)
        {
            var user = await _userManager.GetUserAsync(User);
            var profilePicture = user1.ProfilePicture;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                var user1 = new ApplicationUser {
                    ProfilePicture = Input.ProfilePicture
                };

                await LoadAsync(user);
                return Page();
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }


            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }



            //var avatar = _userManager.

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();



        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using TaskManagementSystem.Models;
//using Task = System.Threading.Tasks.Task;

//namespace TaskManagementSystem.Areas.Identity.Pages.Account.Manage
//{
//    public partial class IndexModel : PageModel
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;

//        public IndexModel(
//            UserManager<ApplicationUser> userManager,
//            SignInManager<ApplicationUser> signInManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }

//        public string Username { get; set; }

//        [TempData]
//        public string StatusMessage { get; set; }

//        [TempData]
//        public string UserNameChangeLimitMessage { get; set; }

//        [BindProperty]
//        public InputModel Input { get; set; }

//        public class InputModel
//        {
//            //[Phone]
//            //[Display(Name = "Phone number")]
//            //public string PhoneNumber { get; set; }

//            [Required(ErrorMessage = "Please Write First Name")]
//            [StringLength(25, ErrorMessage = "First name should be this long", MinimumLength = 2)]
//            [Display(Name = "First Name")]
//            public string FirstName { get; set; }


//            [Required(ErrorMessage = "Please Write Last Name")]
//            [StringLength(25, ErrorMessage = "Last name should be this long", MinimumLength = 2)]
//            [Display(Name = "Last Name")]
//            public string LastName { get; set; }

//            [Required(ErrorMessage = "Please Write User Name")]
//            [StringLength(25, ErrorMessage = "Last name should be this long", MinimumLength = 2)]
//            [Display(Name = "User Name")]
//            public string UserName { get; set; }


//            [Required(ErrorMessage = "Please specify Address")]
//            [StringLength(50, ErrorMessage = " should be this long", MinimumLength = 2)]
//            public string Address { get; set; }


//            [Required]
//            [EmailAddress]
//            [Display(Name = "Email")]


//            public string Email { get; set; }
//            [Display(Name = "Profile Picture")]
//            public byte[] ProfilePicture { get; set; }



//        }

//        private async Task LoadAsync(ApplicationUser user)
//        {
//            var userName = await _userManager.GetUserNameAsync(user);
//            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

//            //var firstName = user.FirstName;
//            //var lastName = user.LastName;

//            var fullName = user.FullName;
//            //var profilePicture = user.ProfilePicture;

//            var address = user.Address;

//            Username = userName;

//            Input = new InputModel
//            {
//                //PhoneNumber = phoneNumber

//                UserName = Input.UserName,

//                FirstName = Input.FirstName,
//                LastName = Input.LastName,
//                Email = Input.Email,
//                Address = Input.Address,
//                //ProfilePicture = profilePicture,



//            };
//        }

//        public async Task<IActionResult> OnGetAsync()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            await LoadAsync(user);
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            if (!ModelState.IsValid)
//            {
//                await LoadAsync(user);
//                return Page();
//            }

//            //var firstName = user.FirstName;
//            //var lastName = user.LastName;
//            //if (Input.FirstName != firstName)
//            //{
//            //    user.FirstName = Input.FirstName;
//            //    await _userManager.UpdateAsync(user);
//            //}
//            //if (Input.LastName != lastName)
//            //{
//            //    user.LastName = Input.LastName;
//            //    await _userManager.UpdateAsync(user);
//            //}


//            //if (Request.Form.Files.Count > 0)
//            //{
//            //    IFormFile file = Request.Form.Files.FirstOrDefault();
//            //    using (var dataStream = new MemoryStream())
//            //    {
//            //        await file.CopyToAsync(dataStream);
//            //        user.ProfilePicture = dataStream.ToArray();
//            //    }
//            //    await _userManager.UpdateAsync(user);
//            //}

//            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
//            //if (Input.PhoneNumber != phoneNumber)
//            //{
//            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
//            //    if (!setPhoneResult.Succeeded)
//            //    {
//            //        StatusMessage = "Unexpected error when trying to set phone number.";
//            //        return RedirectToPage();
//            //    }
//            //}




//            //var avatar = _userManager.

//            await _signInManager.RefreshSignInAsync(user);
//            StatusMessage = "Your profile has been updated";
//            return RedirectToPage();



//        }
//    }
//}
