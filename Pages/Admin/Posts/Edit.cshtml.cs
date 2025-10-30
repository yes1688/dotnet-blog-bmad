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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Id { get; set; }

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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            Id = post.Id;
            Title = post.Title;
            Summary = post.Summary;
            Content = post.Content;
            IsPublished = post.IsPublished;

            // Load existing tags as comma-separated string
            TagInput = string.Join(", ", post.Tags.Select(t => t.Name));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var post = await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == Id);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = Title;
            post.Summary = Summary;
            post.Content = Content;

            // Check if IsPublished changed from false to true
            if (!post.IsPublished && IsPublished)
            {
                post.PublishedAt = DateTime.UtcNow;
            }

            post.IsPublished = IsPublished;
            post.UpdatedAt = DateTime.UtcNow;

            // Update tags
            post.Tags.Clear();
            if (!string.IsNullOrWhiteSpace(TagInput))
            {
                var tagNames = TagInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(t => t.Trim())
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Distinct()
                    .ToList();

                foreach (var tagName in tagNames)
                {
                    var slug = tagName.ToLower()
                        .Replace(" ", "-")
                        .Replace("_", "-");

                    var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Slug == slug);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = tagName,
                            Slug = slug,
                            Color = "blue",
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.Tags.Add(tag);
                    }
                    post.Tags.Add(tag);
                }
            }

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Posts/Index");
        }
    }
}
