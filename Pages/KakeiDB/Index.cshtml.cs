using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HABManagement.Data;
using HABManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HABManagement.Pages.KakeiDB
{
    public class IndexModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public IndexModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }
        public IList<Kakei> Kakei { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        


        public async Task OnGetAsync()
        {
            var kakeis = from m in _context.Kakei
                         select m;

            if (SearchString == "収入")
            {
                kakeis = kakeis.Where(s => s.Balance.Contains(SearchString));
            }
            if (SearchString == "支出")
            {
                kakeis = kakeis.Where(s => s.Balance.Contains(SearchString));
            }
            Kakei = await kakeis.ToListAsync();
        }
    }
}
