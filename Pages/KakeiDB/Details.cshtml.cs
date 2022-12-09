using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HABManagement.Data;
using HABManagement.Models;

namespace HABManagement.Pages.KakeiDB
{
    public class DetailsModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public DetailsModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

      public Kakei Kakei { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Kakei == null)
            {
                return NotFound();
            }

            var kakei = await _context.Kakei.FirstOrDefaultAsync(m => m.ID == id);
            if (kakei == null)
            {
                return NotFound();
            }
            else 
            {
                Kakei = kakei;
            }
            return Page();
        }
    }
}
