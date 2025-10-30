using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            Id = post.Id;
            Title = post.Title;
            Summary = post.Summary;
            Content = post.Content;
            IsPublished = post.IsPublished;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var post = await _context.Posts.FindAsync(Id);

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

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Posts/Index");
        }
    }
}
