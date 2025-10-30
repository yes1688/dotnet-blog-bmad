using dotnet_blog_bmad.Data.Entities;

namespace dotnet_blog_bmad.Services.Interfaces;

/// <summary>
/// 管理員服務介面
/// </summary>
public interface IAdminService
{
    /// <summary>
    /// 檢查 Email 是否在白名單中
    /// </summary>
    /// <param name="email">要檢查的 Email</param>
    /// <returns>如果在白名單中則回傳 true，否則回傳 false</returns>
    Task<bool> IsAdminWhitelistedAsync(string email);

    /// <summary>
    /// 根據 Email 取得管理員
    /// </summary>
    /// <param name="email">管理員 Email</param>
    /// <returns>找到的管理員實體，如果不存在則回傳 null</returns>
    Task<Admin?> GetAdminByEmailAsync(string email);

    /// <summary>
    /// 建立或更新管理員，並更新最後登入時間
    /// </summary>
    /// <param name="email">管理員 Email</param>
    /// <param name="displayName">顯示名稱</param>
    /// <returns>建立或更新後的管理員實體</returns>
    Task<Admin> CreateOrUpdateAdminAsync(string email, string displayName);

    /// <summary>
    /// 取得所有管理員
    /// </summary>
    /// <returns>所有管理員的集合</returns>
    Task<IEnumerable<Admin>> GetAllAdminsAsync();
}
