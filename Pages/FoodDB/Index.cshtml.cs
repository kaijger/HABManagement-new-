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

        
        public const string SessionKeyName = "_Name";

        public IList<Food> Food { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public string? DateSort { get; set; }
        public string? ExpiryDateSort { get; set; }

        public string? NumSort { get; set; }

        
        public async Task OnGetAsync(string sortOrder)
        {
            string yr = DateTime.Today.Year.ToString();
            string mn = DateTime.Today.Month.ToString();
            string dt = DateTime.Today.Day.ToString();
            string datenow = string.Format("{0}-{1}-{2}", yr, mn, dt);

            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ExpiryDateSort = sortOrder == "ExpiryDate" ? "expirydate_desc" : "ExpiryDate";
            NumSort = sortOrder == "Num" ? "num_desc" : "Num";

            IQueryable<Food> itemsIQ = from s in _context.Food
                                      select s;
            if (!string.IsNullOrEmpty(SearchString))
            {
                itemsIQ = from s in _context.Food
                          where s.Name == SearchString
                          select s;
            }
            

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, datenow);

            }
            var name = HttpContext.Session.GetString(SessionKeyName);

            _logger.LogInformation("Session Name: {Name}", name);


            switch (sortOrder)
            {
                case "Date":
                    itemsIQ = itemsIQ.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    itemsIQ = itemsIQ.OrderByDescending(s => s.Date);
                    break;
                case "ExpiryDate":
                    itemsIQ = itemsIQ.OrderBy(s => s.ExpiryDate);
                    break;
                case "expirydate_desc":
                    itemsIQ = itemsIQ.OrderByDescending(s => s.ExpiryDate);
                    break;
                case "Num":
                    itemsIQ = itemsIQ.OrderBy(s => s.Num);
                    break;
                case "num_desc":
                    itemsIQ = itemsIQ.OrderByDescending(s => s.Num);
                    break;
                default:
                    itemsIQ = itemsIQ.OrderBy(s => s.Date);
                    break;
            }

            
            Food = await itemsIQ.AsNoTracking().ToListAsync();
            
        }
    }
}
