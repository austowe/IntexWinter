using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IntexWinter.Models;

namespace IntexWinter.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IntexWinter.Models.intexContext _context;

        public CreateModel(IntexWinter.Models.intexContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Burialmain Burialmain { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Burialmain.Add(Burialmain);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
