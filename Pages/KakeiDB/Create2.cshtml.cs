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
    public class Create2Model : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public Create2Model(HABManagement.Data.HABManagementContext context)
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
                Kakei.Category = "";
            }
            if (Kakei.Balance == "null")
            {
                Kakei.Balance = "収入";
            }
            _context.Kakei.Add(Kakei);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
