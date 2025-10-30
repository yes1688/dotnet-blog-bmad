namespace dotnet_blog_bmad.Data.Entities;

/// <summary>
/// 部落格文章實體
/// </summary>
public class Post
{
    /// <summary>
    /// 文章 ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文章標題
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// URL Slug (SEO 友善的 URL 識別碼)
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// 文章內容 (Markdown 格式)
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 摘要 (可選，用於列表顯示)
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// 是否已發布
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// 發布時間
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// SEO Meta Description
    /// </summary>
    public string? MetaDescription { get; set; }

    /// <summary>
    /// 標籤 (以逗號分隔的字串)
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// 瀏覽次數
    /// </summary>
    public int ViewCount { get; set; }
}
