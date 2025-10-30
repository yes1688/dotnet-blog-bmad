namespace dotnet_blog_bmad.Data.Entities;

/// <summary>
/// 標籤實體
/// </summary>
public class Tag
{
    /// <summary>
    /// 標籤 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 標籤名稱 (唯一)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// URL Slug (用於 URL，例如 "csharp", "dotnet")
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// 顯示顏色 (Tailwind CSS 顏色名稱，例如 "blue", "green")
    /// </summary>
    public string Color { get; set; } = "gray";

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 關聯的文章 (多對多關係)
    /// </summary>
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
