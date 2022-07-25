using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using asp_project.Data;
using asp_project.Models;

namespace asp_project.Pages.test
{
    public class CreateModel : PageModel
    {
        private readonly asp_project.Data.ApplicationDbContext _context;

        public CreateModel(asp_project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Nationality"] = new SelectList(_context.Nationalities, "Name", "Name");
        ViewData["Skills"] = new SelectList(_context.Skills, "Name", "Name");
            return Page();
        }

        [BindProperty]
        public CVModel CVModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CVModels == null || CVModel == null)
            {
                return Page();
            }

            _context.CVModels.Add(CVModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
