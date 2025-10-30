using System.ComponentModel.DataAnnotations;
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
        public List<Comment> Comments { get; set; } = new();

        [BindProperty]
        [Required(ErrorMessage = "請輸入您的名稱")]
        public string CommentAuthorName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "請輸入您的 Email")]
        [EmailAddress(ErrorMessage = "Email 格式不正確")]
        public string CommentAuthorEmail { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "請輸入評論內容")]
        public string CommentContent { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

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
            Comments = post.Comments.OrderBy(c => c.CreatedAt).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(id);
            }

            var comment = new Comment
            {
                PostId = id,
                AuthorName = CommentAuthorName,
                AuthorEmail = CommentAuthorEmail,
                Content = CommentContent,
                CreatedAt = DateTime.UtcNow,
                IsApproved = true // Auto-approve for now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Redirect to clear form
            return RedirectToPage(new { id });
        }
    }
}
