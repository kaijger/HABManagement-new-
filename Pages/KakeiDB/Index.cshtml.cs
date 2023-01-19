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
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        public IndexModel(HABManagement.Data.HABManagementContext context, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            Configuration = configuration;
        }
        public PaginatedList<Kakei> Kakei { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public string? DateSort { get; set; }

        public const string SessionKeyName = "Item";
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var kakeis = from m in _context.Kakei
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                HttpContext.Session.SetString(SessionKeyName, SearchString);
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
                case "Date":
                    kakeis = kakeis.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    kakeis = kakeis.OrderByDescending(s => s.Date);
                    break;
                default:
                    kakeis = kakeis.OrderBy(s => s.Date);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Kakei = await PaginatedList<Kakei>.CreateAsync(
                kakeis.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
