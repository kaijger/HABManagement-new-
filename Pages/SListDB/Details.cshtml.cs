using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HABManagement.Data;
using HABManagement.Models;

namespace HABManagement.Pages.SListDB
{
    public class DetailsModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public DetailsModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

      public SList SList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SList == null)
            {
                return NotFound();
            }

            var slist = await _context.SList.FirstOrDefaultAsync(m => m.ID == id);
            if (slist == null)
            {
                return NotFound();
            }
            else 
            {
                SList = slist;
            }
            return Page();
        }
    }
}
