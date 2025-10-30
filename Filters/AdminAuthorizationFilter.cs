using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using dotnet_blog_bmad.Services.Interfaces;
using System.Security.Claims;

namespace dotnet_blog_bmad.Filters;

/// <summary>
/// Admin 頁面授權過濾器
/// 用於檢查使用者是否已登入且在管理員白名單中
/// </summary>
public class AdminAuthorizationFilter : IPageFilter
{
    private readonly IAdminService _adminService;

    /// <summary>
    /// 初始化 AdminAuthorizationFilter
    /// </summary>
    /// <param name="adminService">管理員服務</param>
    public AdminAuthorizationFilter(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// 在頁面處理常式選擇後執行
    /// </summary>
    /// <param name="context">頁面處理常式選擇上下文</param>
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        // 不需要實作
    }

    /// <summary>
    /// 在頁面處理常式執行前執行
    /// 檢查使用者是否已登入且在管理員白名單中
    /// </summary>
    /// <param name="context">頁面處理常式執行上下文</param>
    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var user = httpContext.User;

        // 檢查使用者是否已驗證
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new RedirectResult("/Admin/Login");
            return;
        }

        // 取得使用者的 email claim
        var emailClaim = user.FindFirst(ClaimTypes.Email) ?? user.FindFirst("email");
        if (emailClaim == null || string.IsNullOrWhiteSpace(emailClaim.Value))
        {
            context.Result = new RedirectResult("/Admin/Login");
            return;
        }

        // 檢查是否在管理員白名單中
        var isWhitelisted = _adminService.IsAdminWhitelistedAsync(emailClaim.Value).GetAwaiter().GetResult();
        if (!isWhitelisted)
        {
            context.Result = new RedirectResult("/Admin/Login");
            return;
        }

        // 驗證通過，繼續執行
    }

    /// <summary>
    /// 在頁面處理常式執行後執行
    /// </summary>
    /// <param name="context">頁面處理常式執行後上下文</param>
    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        // 不需要實作
    }
}

/// <summary>
/// Admin 授權屬性
/// 用於標記需要管理員權限的 Razor Pages
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class AdminAuthorizationAttribute : Attribute, IFilterFactory
{
    /// <summary>
    /// 是否可重複使用
    /// </summary>
    public bool IsReusable => false;

    /// <summary>
    /// 建立過濾器實例
    /// </summary>
    /// <param name="serviceProvider">服務提供者</param>
    /// <returns>AdminAuthorizationFilter 實例</returns>
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var adminService = serviceProvider.GetRequiredService<IAdminService>();
        return new AdminAuthorizationFilter(adminService);
    }
}
