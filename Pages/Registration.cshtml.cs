using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCTV_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CCTV_App.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly CCTV_AppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegistrationModel(CCTV_AppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IList<User> ListUsers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            bool role = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (role == false)
            {
                return RedirectToPage("/UnAuthorized");
            }
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/UnAuthorized");
            }
            ListUsers = await _context.Users.ToListAsync();
            return Page();
        }

        [BindProperty]
        public new User User { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        public async Task<IActionResult> OnPostAsync()
        {
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            bool role = _httpContextAccessor.HttpContext.User.IsInRole("admin");
            if (!role)
            {
                return RedirectToPage("./UnAuthorized");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (User.Password != User.ConfirmPassword)
            {
                ModelState.AddModelError("", "Password does not Match");
                return Page();
            }
            if (_context.Users.Any(x=>x.Username.Equals(User.Username)))
            {
                ModelState.AddModelError("", "Username not available");
                return Page();
            }
            var passwordHasher = new PasswordHasher<string>();
            User.Password = passwordHasher.HashPassword(null, User.Password);
            User.ConfirmPassword = passwordHasher.HashPassword(null, User.Password);
            User.CreatedDate = DateTime.Now; 
            User.CreatedBy = userName;
            
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
