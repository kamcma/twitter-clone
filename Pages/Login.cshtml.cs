using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TwitterClone.Data;
using TwitterClone.Data.Models;

namespace TwitterClone.Pages
{
    public class LoginPage : PageModel
    {
        private readonly TwitterCloneContext db;

        public LoginPage(TwitterCloneContext db)
        {
            this.db = db;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            User user = await db.Users
                .FirstAsync(u => u.EmailAddress == EmailAddress);

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            if (user == null || Password == null
                || hasher.VerifyHashedPassword(user, user.PasswordHash, Password) != PasswordVerificationResult.Success)
            {
                return Page();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }
    }
}