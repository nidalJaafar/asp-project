using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_project.Data;
using asp_project.Models;

namespace asp_project.Pages.test
{
    public class EditModel : PageModel
    {
        private readonly asp_project.Data.ApplicationDbContext _context;

        public EditModel(asp_project.Data.ApplicationDbContext context)
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

            var cvmodel =  await _context.CVModels.FirstOrDefaultAsync(m => m.Id == id);
            if (cvmodel == null)
            {
                return NotFound();
            }
            CVModel = cvmodel;
           ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CVModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CVModelExists(CVModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CVModelExists(int id)
        {
          return (_context.CVModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
