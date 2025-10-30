namespace dotnet_blog_bmad.Data.Entities;

/// <summary>
/// 管理員實體
/// </summary>
public class Admin
{
    /// <summary>
    /// 管理員 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Google Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 顯示名稱
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// 是否啟用
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 最後登入時間
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
