using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ProjectGroup.Pages.Authentication
{
    public class Logout : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();

            return RedirectToPage("/TicketsPage/Index");
        }
    }
}