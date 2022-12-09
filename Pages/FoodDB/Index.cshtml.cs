using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HABManagement.Data;
using HABManagement.Models;

namespace HABManagement.Pages.FoodDB
{
    public class IndexModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public IndexModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Food != null)
            {
                Food = await _context.Food.ToListAsync();
            }
        }
    }
}
