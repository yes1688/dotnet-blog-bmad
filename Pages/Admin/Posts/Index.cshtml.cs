using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;
using dotnet_blog_bmad.Filters;

namespace dotnet_blog_bmad.Pages.Admin.Posts;

/// <summary>
/// 文章列表頁面模型
/// </summary>
[AdminAuthorization]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// 建構函式，注入資料庫上下文
    /// </summary>
    /// <param name="context">資料庫上下文</param>
    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 文章列表
    /// </summary>
    public List<Post> Posts { get; set; } = new();

    /// <summary>
    /// GET 請求處理：載入所有文章
    /// </summary>
    public async Task OnGetAsync()
    {
        // 查詢所有文章，按發布時間降序排列（未發布的排在最後），然後按建立時間降序排列
        Posts = await _context.Posts
            .OrderByDescending(p => p.PublishedAt ?? DateTime.MinValue)
            .ThenByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}
