using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    public class IndexPage : PageModel
    {
        private readonly TwitterCloneContext db;

        public IndexPage(TwitterCloneContext db)
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

        [BindProperty]
        [StringLength(256)]
        [Required]
        public string NewTweetBody { get; set; }

        [BindProperty]
        public List<Tweet> Tweets { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Tweets = await db.Tweets.OrderByDescending(t => t.Timestamp)
                .Page(pageIndex ?? 0, 30).ToListAsync();
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid) { return Page(); }
            
            User user = await db.Users
                .FirstAsync(u => u.EmailAddress == EmailAddress);

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            if (user == null
                || hasher.VerifyHashedPassword(user, user.PasswordHash, Password) != PasswordVerificationResult.Success)
            {
                EmailAddress = null;
                Password = null;
                return Page();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, EmailAddress));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction(nameof(IndexPage.OnGetAsync));
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(IndexPage.OnGetAsync));
        }

        public async Task<IActionResult> OnPostTweetAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            var authorEmail = HttpContext.User.Claims
                .First(c => c.Type == ClaimTypes.Email).Value;
            var author = await db.Users
                .FirstAsync(u => u.EmailAddress == authorEmail);

            var newTweet = new Tweet
            {
                Body = NewTweetBody,
                Author = author,
                Timestamp = DateTime.UtcNow
            };

            db.Tweets.Add(newTweet);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(IndexPage.OnGetAsync));
        }
    }
}