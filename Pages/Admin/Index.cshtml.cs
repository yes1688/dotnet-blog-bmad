using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Services.Interfaces;
using dotnet_blog_bmad.Filters;

namespace dotnet_blog_bmad.Pages.Admin;

/// <summary>
/// 管理後台首頁 PageModel
/// </summary>
[AdminAuthorization]
public class IndexModel : PageModel
{
    private readonly IAdminService _adminService;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// 初始化 IndexModel
    /// </summary>
    /// <param name="adminService">管理員服務</param>
    /// <param name="context">資料庫上下文</param>
    public IndexModel(IAdminService adminService, ApplicationDbContext context)
    {
        _adminService = adminService;
        _context = context;
    }

    /// <summary>
    /// 總文章數
    /// </summary>
    public int TotalPosts { get; set; }

    /// <summary>
    /// 已發布文章數
    /// </summary>
    public int PublishedPosts { get; set; }

    /// <summary>
    /// 草稿文章數
    /// </summary>
    public int DraftPosts { get; set; }

    /// <summary>
    /// 載入首頁資料
    /// </summary>
    public async Task OnGetAsync()
    {
        // 查詢資料庫計算統計數字
        TotalPosts = await _context.Posts.CountAsync();
        PublishedPosts = await _context.Posts.CountAsync(p => p.IsPublished);
        DraftPosts = TotalPosts - PublishedPosts;
    }
}
