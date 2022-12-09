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

namespace HABManagement.Pages.KakeiDB
{
    public class EditModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public EditModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Kakei Kakei { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Kakei == null)
            {
                return NotFound();
            }

            var kakei =  await _context.Kakei.FirstOrDefaultAsync(m => m.ID == id);
            if (kakei == null)
            {
                return NotFound();
            }
            Kakei = kakei;
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

            _context.Attach(Kakei).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KakeiExists(Kakei.ID))
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

        private bool KakeiExists(int id)
        {
          return _context.Kakei.Any(e => e.ID == id);
        }
    }
}
