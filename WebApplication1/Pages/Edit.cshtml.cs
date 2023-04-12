using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntexWinter.Models;

namespace IntexWinter.Pages
{
    public class EditModel : PageModel
    {
        private readonly IntexWinter.Models.intexContext _context;

        public EditModel(IntexWinter.Models.intexContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Burialmain Burialmain { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Burialmain = await _context.Burialmain.FirstOrDefaultAsync(m => m.Id == id);

            if (Burialmain == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Burialmain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurialmainExists(Burialmain.Id))
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

        private bool BurialmainExists(long id)
        {
            return _context.Burialmain.Any(e => e.Id == id);
        }
    }
}
