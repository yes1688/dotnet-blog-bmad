namespace dotnet_blog_bmad.Helpers;

/// <summary>
/// SEO 輔助工具類別
/// </summary>
public static class SeoHelper
{
    /// <summary>
    /// 生成 Meta Description
    /// </summary>
    public static string GenerateMetaDescription(string? metaDescription, string? summary, string content, int maxLength = 160)
    {
        if (!string.IsNullOrWhiteSpace(metaDescription))
            return metaDescription.Length > maxLength
                ? metaDescription.Substring(0, maxLength)
                : metaDescription;

        if (!string.IsNullOrWhiteSpace(summary))
            return summary.Length > maxLength
                ? summary.Substring(0, maxLength)
                : summary;

        var text = SearchHelper.ExtractTextFromMarkdown(content, maxLength);
        return text.Length > maxLength
            ? text.Substring(0, maxLength)
            : text;
    }

    /// <summary>
    /// 清理文字用於 SEO（移除特殊字元）
    /// </summary>
    public static string CleanForSeo(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Remove HTML tags
        text = System.Text.RegularExpressions.Regex.Replace(text, "<.*?>", string.Empty);

        // Remove special characters but keep basic punctuation
        text = System.Text.RegularExpressions.Regex.Replace(text, @"[^\w\s\.,!?;:\-\u4e00-\u9fff]", "");

        // Normalize whitespace
        text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ").Trim();

        return text;
    }

    /// <summary>
    /// 生成結構化資料（Schema.org）
    /// </summary>
    public static string GenerateArticleSchema(string title, string description, string authorName, DateTime publishedDate, DateTime modifiedDate, string imageUrl, string articleUrl)
    {
        return $@"
        {{
            ""@context"": ""https://schema.org"",
            ""@type"": ""BlogPosting"",
            ""headline"": ""{System.Net.WebUtility.HtmlEncode(title)}"",
            ""description"": ""{System.Net.WebUtility.HtmlEncode(description)}"",
            ""author"": {{
                ""@type"": ""Person"",
                ""name"": ""{System.Net.WebUtility.HtmlEncode(authorName)}""
            }},
            ""datePublished"": ""{publishedDate:yyyy-MM-ddTHH:mm:ssZ}"",
            ""dateModified"": ""{modifiedDate:yyyy-MM-ddTHH:mm:ssZ}"",
            ""image"": ""{imageUrl}"",
            ""url"": ""{articleUrl}""
        }}";
    }
}
