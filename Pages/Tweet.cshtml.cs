using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitterClone.Data;
using TwitterClone.Data.Models;

namespace TwitterClone.Pages
{
    public class TweetPage : PageModel
    {
        private readonly TwitterCloneContext db;

        public TweetPage(TwitterCloneContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Tweet Tweet { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Tweet = await db.Tweets.FindAsync(id);

            if (Tweet == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
