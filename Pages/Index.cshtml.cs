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
    public string? SelectedTag { get; set; }
    public string? SearchQuery { get; set; }
    public int SearchResultCount { get; set; }

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync(int pageNumber = 1, string? tag = null, string? search = null)
    {
        // Cache for 5 minutes
        Response.Headers.CacheControl = "public, max-age=300";

        CurrentPage = pageNumber < 1 ? 1 : pageNumber;
        SelectedTag = tag;
        SearchQuery = search;

        // Build query - start with published posts
        var query = _context.Posts
            .Include(p => p.Tags)
            .Where(p => p.IsPublished);

        // Apply tag filter if specified
        if (!string.IsNullOrEmpty(tag))
        {
            query = query.Where(p => p.Tags.Any(t => t.Slug == tag));
        }

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchLower) ||
                p.Content.ToLower().Contains(searchLower) ||
                (p.Summary != null && p.Summary.ToLower().Contains(searchLower))
            );
        }

        // Count total posts (after filter)
        TotalPosts = await query.CountAsync();

        // Calculate total pages
        TotalPages = (int)Math.Ceiling(TotalPosts / (double)PageSize);
        SearchResultCount = TotalPosts;

        // Ensure current page is within bounds
        if (CurrentPage > TotalPages && TotalPages > 0)
        {
            CurrentPage = TotalPages;
        }

        // Load posts for current page
        Posts = await query
            .OrderByDescending(p => p.PublishedAt)
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}
