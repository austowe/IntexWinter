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
    public class IndexModel : PageModel
    {
        private readonly IntexWinter.Models.intexContext _context;

        public IndexModel(IntexWinter.Models.intexContext context)
        {
            _context = context;
        }

        public IList<Burialmain> Burialmain { get;set; }

        public async Task OnGetAsync()
        {
            Burialmain = await _context.Burialmain.ToListAsync();
        }
    }
}
