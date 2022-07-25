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
    public class DeleteModel : PageModel
    {
        private readonly asp_project.Data.ApplicationDbContext _context;

        public DeleteModel(asp_project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CVModel CVModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CVModels == null)
            {
                return NotFound();
            }

            var cvmodel = await _context.CVModels.FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CVModels == null)
            {
                return NotFound();
            }
            var cvmodel = await _context.CVModels.FindAsync(id);

            if (cvmodel != null)
            {
                CVModel = cvmodel;
                _context.CVModels.Remove(CVModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
