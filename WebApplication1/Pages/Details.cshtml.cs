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
    public class DetailsModel : PageModel
    {
        private readonly IntexWinter.Models.intexContext _context;

        public DetailsModel(IntexWinter.Models.intexContext context)
        {
            _context = context;
        }

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
    }
}
