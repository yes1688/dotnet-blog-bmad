using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnet_blog_bmad.Pages.Admin;

/// <summary>
/// 登出頁面模型
/// </summary>
public class LogoutModel : PageModel
{
    /// <summary>
    /// 處理登出請求
    /// </summary>
    /// <returns>重導向到首頁</returns>
    public async Task<IActionResult> OnGet()
    {
        // 登出 Cookie 認證
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // 登出 Google 認證
        await HttpContext.SignOutAsync("Google");

        // 重導向到首頁
        return Redirect("/");
    }
}
