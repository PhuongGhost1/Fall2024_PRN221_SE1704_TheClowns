using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketResell_BusinessObject;
using TicketResell_Service;
using TicketResell_Service.Utilities;

namespace ProjectGroup.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public RegisterModel(IUserService userService, IConfiguration configuration, EmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string token)
        {
            var email = SecurityHelper.ValidateVerificationToken(token, _configuration["JwtSettings:SecretKey"]);
            if (email == null)
            {
                ViewData["Message"] = "Verification failed. Invalid or expired token.";
                return Page();
            }

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found. Please try again!!!";
                return Page();
            }

            user.IsVerified = true;

            var success = await _userService.UpdateUserAsync(user);
            if (success)
            {
                TempData["SuccessMessage"] = "Register successfully! Welcome to our system";
                return Page();
            }
            else
            {
                TempData["ErrorMessage"] = "Register Failed! Please try again!!!";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emailExists = await _userService.IsEmailExistAsync(Input.Email);
            if (emailExists)
            {
                TempData["ErrorMessage"] = "Email is already in use. Please use a different email.";
                return Page();
            }

            var user = new User
            {
                Username = Input.UserName,
                Email = Input.Email,
                Password = Input.Password
            };

            var isRegister = await _userService.RegisterUserAsync(user);
            if (isRegister)
            {
                var token = SecurityHelper.GenerateVerificationToken(Input.Email, _configuration["JwtSettings:SecretKey"]);

                var htmlBody = $"<h3>Click the link below to verify your email address:</h3><a href=\"http://localhost:5157/Authentication/Register?token={token}\">Verify Email</a>";

                var isSending = await _emailService.SendVerificationEmail(Input.Email, token, "Email Verification", htmlBody);
                if (isSending)
                {
                    TempData["SuccessMessage"] = "Please check your email to verify your account.";
                    return Page();
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try again.";
                    return Page();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Cannot registered. Please try again.";
                return Page();
            }
        }
    }
}
