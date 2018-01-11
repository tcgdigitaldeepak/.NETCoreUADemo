using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.WishList
{
    public class EditModel : PageModel
    {
        private readonly Web.Models.Context _context;

        public EditModel(Web.Models.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public WishlistItem item { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            item = await _context.WishlistItem.SingleOrDefaultAsync(m => m.id == Convert.ToString(id));

            if (item == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("./Index");
        }
    }
}
