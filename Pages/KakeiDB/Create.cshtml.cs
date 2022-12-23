using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HABManagement.Data;
using HABManagement.Models;
using Microsoft.Extensions.Logging;

namespace HABManagement.Pages.KakeiDB
{
    public class CreateModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;
        private readonly ILogger<IndexModel> _logger;
        public CreateModel(HABManagement.Data.HABManagementContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public const string SessionKeyName = "_Name";

        public IActionResult OnGet()
        {
            string yr = DateTime.Today.Year.ToString();
            string mn = DateTime.Today.Month.ToString();
            string dt = DateTime.Today.Day.ToString();
            string datenow = string.Format("{0}-{1}-{2}", yr, mn, dt);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, datenow);

            }
            var name = HttpContext.Session.GetString(SessionKeyName);

            _logger.LogInformation("Session Name: {Name}", name);
            return Page();
        }

        [BindProperty]
        public Kakei Kakei { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
			if (Kakei.Category == "null")
			{
				Kakei.Category = "";
			}
			if (Kakei.Balance == "null")
			{
				Kakei.Balance = "支出";
			}

			_context.Kakei.Add(Kakei);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
