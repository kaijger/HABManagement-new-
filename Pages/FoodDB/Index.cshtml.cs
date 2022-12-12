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
        private readonly HABManagement.Data.HABManagementContext _context;

        public IndexModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public string DateSort { get; set; }
        public string ExpiryDateSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var reizouko = from m in _context.Food
                           select m;

            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ExpiryDateSort = sortOrder == "ExpiryDate" ? "expirydate_desc" : "ExpiryDate";
            IQueryable<Food> itemsIQ = from s in _context.Food
                                       select s;

            if (!string.IsNullOrEmpty(SearchString))
            {
                reizouko = reizouko.Where(s => s.Name.Contains(SearchString));
                Food = await reizouko.ToListAsync();
            }
            else
            {
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
                    default:
                        itemsIQ = itemsIQ.OrderBy(s => s.Date);
                        break;
                }



                Food = await itemsIQ.AsNoTracking().ToListAsync();
            }

           
            
            
        }
    }
}
