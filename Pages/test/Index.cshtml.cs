using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using asp_project.Data;
using asp_project.Models;

namespace asp_project.Pages.test
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CVModel> CVModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CVModels != null)
            {
                CVModel = await _context.CVModels
                .Include(c => c.Nationality).ToListAsync();
            }
        }
    }
}
