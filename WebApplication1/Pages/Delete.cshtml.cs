using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IntexWinter.Models;

namespace IntexWinter.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IntexWinter.Models.intexContext _context;

        public DeleteModel(IntexWinter.Models.intexContext context)
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Burialmain = await _context.Burialmain.FindAsync(id);

            if (Burialmain != null)
            {
                _context.Burialmain.Remove(Burialmain);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
