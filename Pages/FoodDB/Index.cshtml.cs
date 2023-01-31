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
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace HABManagement.Pages.FoodDB
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HABManagement.Data.HABManagementContext _context;
        public IndexModel(HABManagement.Data.HABManagementContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Food> Food { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public string? DateSort { get; set; }
        public string? ExpiryDateSort { get; set; }

        public string? NumSort { get; set; }

        public const string SessionKeyName = "Item";


        public async Task OnGetAsync(string sortOrder)
        {
            IQueryable<Food> itemsIQ = from s in _context.Food
                                      select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                HttpContext.Session.SetString(SessionKeyName, SearchString);
                itemsIQ = itemsIQ.Where(s=> s.Name.Contains(SearchString));
            }


            switch (sortOrder)
            {
                case "DateAsc":
                    itemsIQ = itemsIQ.OrderBy(s => s.Date);
                    break;
                case "DateDesc":
                    itemsIQ = itemsIQ.OrderByDescending(s => s.Date);
                    break;
                case "ExpiryDateAsc":
                    itemsIQ = itemsIQ.OrderBy(s => s.ExpiryDate);
                    break;
                case "ExpiryDateDesc":
                    itemsIQ = itemsIQ.OrderByDescending(s => s.ExpiryDate);
                    break;
                case "NumAsc":
                    itemsIQ = itemsIQ.OrderBy(s => s.Num);
                    break;
                case "NumDesc":
                    itemsIQ = itemsIQ.OrderByDescending(s => s.Num);
                    break;
                default:
                    itemsIQ = itemsIQ.OrderByDescending(s => s.Date);
                    break;
            }

            
            Food = await itemsIQ.AsNoTracking().ToListAsync();
            
        }
    }
}
