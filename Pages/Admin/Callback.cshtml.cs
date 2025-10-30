using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dotnet_blog_bmad.Services.Interfaces;

namespace dotnet_blog_bmad.Pages.Admin;

/// <summary>
/// Google OAuth 認證回調頁面模型
/// 處理 Google 認證後的回調邏輯，包含白名單驗證與管理員資料更新
/// </summary>
public class CallbackModel : PageModel
{
    private readonly IAdminService _adminService;

    /// <summary>
    /// 建構函式，注入管理員服務
    /// </summary>
    /// <param name="adminService">管理員服務介面</param>
    public CallbackModel(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// GET 請求處理：處理 Google OAuth 認證回調
    /// </summary>
    /// <returns>
    /// 認證成功且在白名單內：重導向到 /Admin/Index
    /// 認證失敗：重導向到 /Admin/Login?error=auth_failed
    /// 不在白名單：重導向到 /Admin/Login?error=not_authorized
    /// </returns>
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            // 檢查認證結果
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 若認證失敗，重導向到登入頁面並顯示錯誤訊息
            if (!authenticateResult.Succeeded)
            {
                return Redirect("/Admin/Login?error=auth_failed");
            }

            // 取得使用者的 Email Claim
            var email = User.FindFirst(ClaimTypes.Email)?.Value
                        ?? User.FindFirst("email")?.Value;

            // 若無法取得 Email，視為認證失敗
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("/Admin/Login?error=auth_failed");
            }

            // 檢查 Email 是否在管理員白名單中
            var isWhitelisted = await _adminService.IsAdminWhitelistedAsync(email);

            if (!isWhitelisted)
            {
                // 不在白名單，登出並重導向到登入頁面
                await HttpContext.SignOutAsync();
                return Redirect("/Admin/Login?error=not_authorized");
            }

            // 取得使用者的 Name Claim
            var displayName = User.FindFirst(ClaimTypes.Name)?.Value
                              ?? User.FindFirst("name")?.Value
                              ?? email; // 如果沒有 name，使用 email 作為顯示名稱

            // 建立或更新管理員資料，並更新最後登入時間
            await _adminService.CreateOrUpdateAdminAsync(email, displayName);

            // 認證成功，重導向到管理後台首頁
            return Redirect("/Admin/Index");
        }
        catch (Exception ex)
        {
            // 發生例外時，記錄錯誤並重導向到登入頁面
            // 在實際專案中，應該使用 ILogger 記錄例外資訊
            Console.Error.WriteLine($"OAuth Callback Error: {ex.Message}");
            return Redirect("/Admin/Login?error=auth_failed");
        }
    }
}
