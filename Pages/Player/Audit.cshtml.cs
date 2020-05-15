using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CCTV_App.Models;
using Microsoft.AspNetCore.Http;

namespace CCTV_App.Pages.Player
{
    public class AuditModel : PageModel
    {
        private readonly CCTV_App.Models.CCTV_AppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuditModel(CCTV_App.Models.CCTV_AppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Audit> Audit { get;set; }

        public async Task<IActionResult> OnGetAsync()
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
            Audit = await _context.Audits.ToListAsync();
            return Page();
        }
    }
}
