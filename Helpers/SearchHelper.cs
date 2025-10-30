using System.Text.RegularExpressions;

namespace dotnet_blog_bmad.Helpers;

/// <summary>
/// 搜尋輔助工具類別
/// </summary>
public static class SearchHelper
{
    /// <summary>
    /// 高亮顯示搜尋關鍵字
    /// </summary>
    /// <param name="text">原始文字</param>
    /// <param name="searchQuery">搜尋關鍵字</param>
    /// <param name="maxLength">最大長度（0 表示不限制）</param>
    /// <returns>高亮處理後的 HTML 字串</returns>
    public static string HighlightSearchTerm(string text, string? searchQuery, int maxLength = 0)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(searchQuery))
        {
            return maxLength > 0 && text.Length > maxLength
                ? text.Substring(0, maxLength) + "..."
                : text;
        }

        // Escape HTML first
        text = System.Net.WebUtility.HtmlEncode(text);
        searchQuery = System.Net.WebUtility.HtmlEncode(searchQuery);

        // Truncate if needed, trying to include the search term
        if (maxLength > 0 && text.Length > maxLength)
        {
            var index = text.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase);
            if (index > 0)
            {
                // Center the excerpt around the search term
                var start = Math.Max(0, index - maxLength / 2);
                var length = Math.Min(maxLength, text.Length - start);
                text = (start > 0 ? "..." : "") + text.Substring(start, length) + "...";
            }
            else
            {
                text = text.Substring(0, maxLength) + "...";
            }
        }

        // Highlight the search term
        var pattern = Regex.Escape(searchQuery);
        var highlighted = Regex.Replace(
            text,
            pattern,
            match => $"<mark class=\"bg-yellow-200 font-semibold\">{match.Value}</mark>",
            RegexOptions.IgnoreCase
        );

        return highlighted;
    }

    /// <summary>
    /// 從 Markdown 內容中提取純文字摘要
    /// </summary>
    public static string ExtractTextFromMarkdown(string markdown, int maxLength = 300)
    {
        if (string.IsNullOrWhiteSpace(markdown))
            return string.Empty;

        // Remove code blocks
        markdown = Regex.Replace(markdown, @"```[\s\S]*?```", "");
        markdown = Regex.Replace(markdown, @"`[^`]*`", "");

        // Remove headers
        markdown = Regex.Replace(markdown, @"^#+\s+", "", RegexOptions.Multiline);

        // Remove links but keep text
        markdown = Regex.Replace(markdown, @"\[([^\]]+)\]\([^\)]+\)", "$1");

        // Remove images
        markdown = Regex.Replace(markdown, @"!\[([^\]]*)\]\([^\)]+\)", "");

        // Remove bold/italic
        markdown = Regex.Replace(markdown, @"[*_]{1,2}([^*_]+)[*_]{1,2}", "$1");

        // Remove extra whitespace
        markdown = Regex.Replace(markdown, @"\s+", " ").Trim();

        if (markdown.Length > maxLength)
            return markdown.Substring(0, maxLength) + "...";

        return markdown;
    }
}
