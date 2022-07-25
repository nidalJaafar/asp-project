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
    public class DetailsModel : PageModel
    {
        private readonly asp_project.Data.ApplicationDbContext _context;

        public DetailsModel(asp_project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public CVModel CVModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CVModels == null)
            {
                return NotFound();
            }

            var cvmodel = await _context.CVModels.Include(c => c.Nationality).FirstOrDefaultAsync(m => m.Id == id);
            if (cvmodel == null)
            {
                return NotFound();
            }
            else 
            {
                CVModel = cvmodel;
            }
            return Page();
        }
    }
}
