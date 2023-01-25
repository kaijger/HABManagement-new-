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
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Configuration;

namespace HABManagement.Pages.KakeiDB
{
    public class IndexModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;
        private readonly IConfiguration Configuration;
        public IndexModel(HABManagement.Data.HABManagementContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public PaginatedList<Kakei> Kakei { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public string? sortOrder { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {

            CurrentSort = sortOrder;
            
            var kakeis = from m in _context.Kakei
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                if (SearchString == "収入")
                {
                    kakeis = kakeis.Where(s => s.Balance.Contains(SearchString));
                }
                if (SearchString == "支出")
                {
                    kakeis = kakeis.Where(s => s.Balance.Contains(SearchString));
                }
            }
            
             switch (sortOrder)
            {
                case "DateAsc":
                    kakeis = kakeis.OrderBy(s => s.Date);
                    break;
                case "DateDesc":
                    kakeis = kakeis.OrderByDescending(s => s.Date);
                    break;
                default:
                    kakeis = kakeis.OrderByDescending(s => s.Date);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Kakei = await PaginatedList<Kakei>.CreateAsync(
                kakeis.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
