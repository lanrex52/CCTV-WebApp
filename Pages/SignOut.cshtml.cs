using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCTV_App.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CCTV_App.Pages
{
    public class SignOutModel : PageModel
    {
        private readonly CCTV_AppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SignOutModel(CCTV_AppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnPost()
        {
            string timeout = DateTime.Now.ToString();
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _context.Audits.Where(s => s.UserName == userName)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();
            user.SignoutTime = timeout;  
            _context.Entry(user).Property(x => x.SignoutTime).IsModified = true;
            await _context.SaveChangesAsync();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
    }
}