using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HABManagement.Data;
using HABManagement.Models;

namespace HABManagement.Pages.SListDB
{
    public class EditModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public EditModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SList SList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SList == null)
            {
                return NotFound();
            }

            var slist =  await _context.SList.FirstOrDefaultAsync(m => m.ID == id);
            if (slist == null)
            {
                return NotFound();
            }
            SList = slist;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SListExists(SList.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SListExists(int id)
        {
          return _context.SList.Any(e => e.ID == id);
        }
    }
}
