using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dotnet_blog_bmad.Services.Interfaces;

namespace dotnet_blog_bmad.Pages.Admin;

/// <summary>
/// 管理員登入頁面模型
/// </summary>
public class LoginModel : PageModel
{
    private readonly IAdminService _adminService;

    /// <summary>
    /// 建構函式，注入管理員服務
    /// </summary>
    /// <param name="adminService">管理員服務介面</param>
    public LoginModel(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// GET 請求處理：檢查使用者是否已登入
    /// </summary>
    /// <returns>如果已登入則重導向到 /Admin/Index，否則顯示登入頁面</returns>
    public IActionResult OnGet()
    {
        // 檢查使用者是否已通過身份驗證
        if (User.Identity?.IsAuthenticated == true)
        {
            // 已登入，重導向到管理後台首頁
            return Redirect("/Admin/Index");
        }

        // 未登入，顯示登入頁面
        return Page();
    }

    /// <summary>
    /// POST 請求處理：觸發 Google OAuth 認證流程
    /// </summary>
    /// <returns>重導向到 Google OAuth 認證頁面</returns>
    public IActionResult OnPostAsync()
    {
        // 觸發 Google OAuth Challenge
        // 認證成功後會重導向到 /Admin/Callback 進行後續處理
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/Admin/Callback"
        };

        return Challenge(properties, "Google");
    }
}
