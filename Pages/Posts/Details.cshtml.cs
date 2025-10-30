using System.Threading.Tasks;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace dotnet_blog_bmad.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Post? Post { get; set; }
        public string RenderedContent { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null || !post.IsPublished)
            {
                return NotFound();
            }

            // Increment view count
            post.ViewCount++;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            // Convert markdown to HTML
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            RenderedContent = Markdown.ToHtml(post.Content, pipeline);

            Post = post;
            return Page();
        }
    }
}
