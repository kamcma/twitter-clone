using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        [StringLength(256)]
        [Required]
        public string NewTweetBody { get; set; }

        [BindProperty]
        public List<Tweet> Tweets { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            Tweets = await db.Tweets.Include(t => t.Author).OrderByDescending(t => t.Timestamp)
                .Page(pageIndex ?? 0, 10).ToListAsync();
        }

        public async Task<IActionResult> OnPostTweetAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            var authorEmail = HttpContext.User.Claims
                .First(c => c.Type == ClaimTypes.Email)?.Value;
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

            return RedirectToPage("/Index");
        }
    }
}