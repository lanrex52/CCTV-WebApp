using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCTV_App.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace CCTV_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CCTV_AppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(CCTV_AppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        public IActionResult OnGetAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/Player/Dashboard");
            }
           
            return Page();
           
        }
        [BindProperty]
        public new User User { get; set; }
       // public  Audit Audit { get; set; }
        public string Message { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.Users.Any(x => x.Username.Equals(User.Username)))
            {
                Audit audit = new Audit();
                string timein = DateTime.Now.ToString();
                var passwordHasher = new PasswordHasher<string>();

                var userDb = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(User.Username));
                
              
                if (passwordHasher.VerifyHashedPassword(null, userDb.Password, User.Password) == PasswordVerificationResult.Success)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, User.Username),
                         new Claim(ClaimTypes.Role, userDb.Role),
                         new Claim(ClaimTypes.GivenName, userDb.FirstName + "" + userDb.LastName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    

                   
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    audit.FirstName = userDb.FirstName;
                    audit.LastName = userDb.LastName;
                    audit.UserName = userDb.Username;
                    audit.Role = userDb.Role;
                    audit.Location = userDb.Location;
                    audit.SigninTime = timein;
                    _context.Audits.Add(audit);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Player/Dashboard");

                }


            }
            Message = "Invalid attempt";
            return Page();


        }
    }
}
