using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitterClone.Data;
using TwitterClone.Data.Models;

namespace TwitterClone.Pages
{
    public class RegisterPage : PageModel
    {
        private readonly TwitterCloneContext db;

        public RegisterPage(TwitterCloneContext db)
        {
            this.db = db;
        }

        [BindProperty]
        [Required]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        public string LastName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            User user = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress
            };
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, Password);

            db.Users.Add(user);
            await db.SaveChangesAsync();
            
            return RedirectToAction(nameof(IndexPage.OnGetAsync));
        }
    }
}