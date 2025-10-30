using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;
using dotnet_blog_bmad.Data.Entities;
using dotnet_blog_bmad.Services.Interfaces;

namespace dotnet_blog_bmad.Services;

/// <summary>
/// 管理員服務實作
/// </summary>
public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 建構子，注入資料庫上下文和設定
    /// </summary>
    /// <param name="context">資料庫上下文</param>
    /// <param name="configuration">應用程式設定</param>
    public AdminService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// 檢查 Email 是否在白名單中
    /// </summary>
    /// <param name="email">要檢查的 Email</param>
    /// <returns>如果在白名單中則回傳 true，否則回傳 false</returns>
    public Task<bool> IsAdminWhitelistedAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Task.FromResult(false);
        }

        // 從設定檔讀取白名單 (逗號分隔的 emails)
        var whitelist = _configuration["Security:AdminWhitelist"];

        if (string.IsNullOrWhiteSpace(whitelist))
        {
            return Task.FromResult(false);
        }

        // 分割白名單並進行比對 (不區分大小寫)
        var whitelistEmails = whitelist
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.Trim())
            .ToList();

        var isWhitelisted = whitelistEmails.Contains(email, StringComparer.OrdinalIgnoreCase);

        return Task.FromResult(isWhitelisted);
    }

    /// <summary>
    /// 根據 Email 取得管理員
    /// </summary>
    /// <param name="email">管理員 Email</param>
    /// <returns>找到的管理員實體，如果不存在則回傳 null</returns>
    public async Task<Admin?> GetAdminByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        return await _context.Admins
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    /// <summary>
    /// 建立或更新管理員，並更新最後登入時間
    /// </summary>
    /// <param name="email">管理員 Email</param>
    /// <param name="displayName">顯示名稱</param>
    /// <returns>建立或更新後的管理員實體</returns>
    public async Task<Admin> CreateOrUpdateAdminAsync(string email, string displayName)
    {
        // 嘗試尋找現有的管理員
        var existingAdmin = await GetAdminByEmailAsync(email);

        if (existingAdmin != null)
        {
            // 更新現有管理員
            existingAdmin.DisplayName = displayName;
            existingAdmin.LastLoginAt = DateTime.UtcNow;

            _context.Admins.Update(existingAdmin);
            await _context.SaveChangesAsync();

            return existingAdmin;
        }
        else
        {
            // 建立新的管理員
            var newAdmin = new Admin
            {
                Email = email,
                DisplayName = displayName,
                IsActive = true,
                LastLoginAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            _context.Admins.Add(newAdmin);
            await _context.SaveChangesAsync();

            return newAdmin;
        }
    }

    /// <summary>
    /// 取得所有管理員
    /// </summary>
    /// <returns>所有管理員的集合</returns>
    public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
    {
        return await _context.Admins
            .Where(a => a.IsActive == true)
            .OrderBy(a => a.Email)
            .ToListAsync();
    }
}
