using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Filters;

namespace dotnet_blog_bmad.Pages.Admin.Posts;

[AdminAuthorization]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return RedirectToPage("/Admin/Posts/Index");
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        TempData["Message"] = "文章已刪除";
        return RedirectToPage("/Admin/Posts/Index");
    }
}
