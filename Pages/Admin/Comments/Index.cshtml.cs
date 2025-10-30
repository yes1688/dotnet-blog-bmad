using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;
using dotnet_blog_bmad.Filters;

namespace dotnet_blog_bmad.Pages.Admin.Comments
{
    [AdminAuthorization]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Comment> Comments { get; set; } = new();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 20;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            CurrentPage = pageNumber;

            var query = _context.Comments
                .Include(c => c.Post)
                .OrderByDescending(c => c.CreatedAt);

            var totalComments = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalComments / (double)PageSize);

            Comments = await query
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
