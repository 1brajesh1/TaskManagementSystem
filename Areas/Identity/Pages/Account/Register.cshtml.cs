using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;
using Task = System.Threading.Tasks.Task;


namespace TaskManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            webHostEnvironment = hostEnvironment;  
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please Write First Name")]
            [StringLength(25, ErrorMessage = "First name should be this long", MinimumLength = 2)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }


            [Required(ErrorMessage = "Please Write Last Name")]
            [StringLength(25, ErrorMessage = "Last name should be this long", MinimumLength = 2)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Please Write User Name")]
            [StringLength(25, ErrorMessage = "Last name should be this long", MinimumLength = 2)]
            [Display(Name = "User Name")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "Please specify Address")]
            [StringLength(50, ErrorMessage = " should be this long", MinimumLength = 2)]
            public string Address { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "User Role")]
            [ForeignKey("UserRole")]
            public string UserRoleId { get; set; }

            public List<SelectListItem> UserRole { get; set; }

            //[Display(Name = "Profile Picture")]
            //public byte[]? ProfilePicture { get; set; }

            //[Required(ErrorMessage = "Please choose profile image")]
            //[Display(Name = "Profile Picture")]
            //public IFormFile ProfileImage { get; set; }

            //[Required(ErrorMessage = "Please choose profile image")]
            //[Display(Name = "Profile Picture")]
            //public byte[] ProfilePicture { get; set; }




        }

        public async Task OnGetAsync(ApplicationUser user,string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var userRoles = _context.Roles.ToList();
            var rolesList = new List<SelectListItem>();

            foreach (var item in userRoles)
            {
                SelectListItem roleLists = new SelectListItem { Value = item.Id, Text = item.Name };
                rolesList.Add(roleLists);
            }
            ViewData["rolesList"] = rolesList;

            //var profilePicture = user.ProfilePicture;

        }



        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.UserName, 
                    FullName = Input.FirstName + " " + Input.LastName,
                    Email = Input.Email,
                    Address = Input.Address,
                    UserRoleId = Input.UserRoleId,
                    //ProfilePicture =Input.ProfilePicture
                };

                //if (Request.Form.Files.Count > 0)
                //{
                //    IFormFile file = Request.Form.Files.FirstOrDefault();
                //    using (var dataStream = new MemoryStream())
                //    {
                //        await file.CopyToAsync(dataStream);
                //        user.ProfilePicture = dataStream.ToArray();
                //    }
                //    await _userManager.UpdateAsync(user);
                //}

               


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (user.UserRoleId == "1")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
                }
                else if (user.UserRoleId == "2")
                {
                    await _userManager.AddToRoleAsync(user, "Manager");
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Manager"));

                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Employee"));

                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
