using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Data;

namespace TwitterClone.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly TwitterCloneContext db;

        public AuthenticationController(TwitterCloneContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
    }
}