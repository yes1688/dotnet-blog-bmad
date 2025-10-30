using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;

namespace dotnet_blog_bmad.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<Post> Posts { get; set; } = new();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPosts { get; set; }

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync(int pageNumber = 1)
    {
        CurrentPage = pageNumber < 1 ? 1 : pageNumber;

        // Count total published posts
        TotalPosts = await _context.Posts
            .Where(p => p.IsPublished)
            .CountAsync();

        // Calculate total pages
        TotalPages = (int)Math.Ceiling(TotalPosts / (double)PageSize);

        // Ensure current page is within bounds
        if (CurrentPage > TotalPages && TotalPages > 0)
        {
            CurrentPage = TotalPages;
        }

        // Load posts for current page
        Posts = await _context.Posts
            .Where(p => p.IsPublished)
            .OrderByDescending(p => p.PublishedAt)
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}
