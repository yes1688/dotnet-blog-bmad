namespace dotnet_blog_bmad.Data.Entities;

/// <summary>
/// 評論實體類別
/// </summary>
public class Comment
{
    /// <summary>
    /// 評論 ID（主鍵）
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文章 ID（外鍵）
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// 評論者名稱
    /// </summary>
    public required string AuthorName { get; set; }

    /// <summary>
    /// 評論者 Email（不公開）
    /// </summary>
    public required string AuthorEmail { get; set; }

    /// <summary>
    /// 評論內容
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// 是否已審核
    /// </summary>
    public bool IsApproved { get; set; } = false;

    /// <summary>
    /// 父文章導航屬性
    /// </summary>
    public Post? Post { get; set; }
}
