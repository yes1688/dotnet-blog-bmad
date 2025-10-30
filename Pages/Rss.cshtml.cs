using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;

namespace dotnet_blog_bmad.Pages
{
    public class RssModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RssModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> Posts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Posts = await _context.Posts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.PublishedAt)
                .Take(20)
                .ToListAsync();

            Response.ContentType = "application/rss+xml";
            return Page();
        }
    }
}
