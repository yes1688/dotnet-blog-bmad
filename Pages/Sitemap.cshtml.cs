using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;

namespace dotnet_blog_bmad.Pages
{
    public class SitemapModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SitemapModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> Posts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Cache sitemap for 1 hour
            Response.Headers.CacheControl = "public, max-age=3600";

            Posts = await _context.Posts
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.UpdatedAt)
                .ToListAsync();

            Response.ContentType = "application/xml";
            return Page();
        }
    }
}
