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
    public class IndexModel : PageModel
    {
        private readonly HABManagement.Data.HABManagementContext _context;

        public IndexModel(HABManagement.Data.HABManagementContext context)
        {
            _context = context;
        }

        public IList<SList> SList { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            IQueryable<SList> SListitem = from s in _context.SList
                                       select s;
            switch (sortOrder)
            {
                case "Asc":
                    SListitem = SListitem.OrderBy(s => s.Priority);
                    break;
                case "Desc":
                    SListitem = SListitem.OrderByDescending(s => s.Priority);
                    break;
                default:
                    SListitem = SListitem.OrderByDescending(s => s.Priority);
                    break;
            }
            SList = await SListitem.AsNoTracking().ToListAsync();

        }
    }
}
