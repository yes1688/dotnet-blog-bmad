using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;
using dotnet_blog_bmad.Filters;

namespace dotnet_blog_bmad.Pages.Admin.Posts
{
    [AdminAuthorization]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "請輸入文章標題")]
        public string Title { get; set; }

        [BindProperty]
        public string Summary { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "請輸入文章內容")]
        public new string Content { get; set; }

        [BindProperty]
        public bool IsPublished { get; set; }

        [BindProperty]
        public string TagInput { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = new Post
            {
                Title = Title,
                Summary = Summary,
                Content = Content,
                IsPublished = IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PublishedAt = IsPublished ? DateTime.UtcNow : null
            };

            // Process tags
            if (!string.IsNullOrWhiteSpace(TagInput))
            {
                var tagNames = TagInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim())
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Distinct()
                    .ToList();

                foreach (var tagName in tagNames)
                {
                    // Generate slug from tag name
                    var slug = tagName.ToLower()
                        .Replace(" ", "-")
                        .Replace("_", "-");

                    // Find or create tag
                    var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Slug == slug);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName,
                            Slug = slug,
                            Color = "blue", // Default color
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.Tags.Add(tag);
                    }
                    post.Tags.Add(tag);
                }
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Posts/Index");
        }
    }
}
