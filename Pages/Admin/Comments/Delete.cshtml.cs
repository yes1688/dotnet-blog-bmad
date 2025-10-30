using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;
using dotnet_blog_bmad.Filters;

namespace dotnet_blog_bmad.Pages.Admin.Comments
{
    [AdminAuthorization]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Comment? Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Comment = await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Comment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
