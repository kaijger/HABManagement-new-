using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HABManagement.Data;
using HABManagement.Models;

namespace HABManagement.Pages.SListDB
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
        public SList SList { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SList.Add(SList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
