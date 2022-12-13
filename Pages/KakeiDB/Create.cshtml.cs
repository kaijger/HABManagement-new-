using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HABManagement.Data;
using HABManagement.Models;

namespace HABManagement.Pages.KakeiDB
{
    public class CreateModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public CreateModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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
                Kakei.Category = Kakei.Category2;
            }
            if (Kakei.Category2 == "null")
            {
                Kakei.Category2 = "";
            }
            if (Kakei.Category == "null" || Kakei.Category2 == "null")
            {
                Kakei.Category = "";
                Kakei.Category2 = "";
            }

            _context.Kakei.Add(Kakei);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
