using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_blog_bmad.Data.Entities;

namespace dotnet_blog_bmad.Data.Configurations;

/// <summary>
/// Post 實體的 Fluent API 配置
/// </summary>
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // 資料表名稱
        builder.ToTable("Posts");

        // 主鍵
        builder.HasKey(p => p.Id);

        // 屬性配置
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Content)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(p => p.Summary)
            .HasMaxLength(500);

        builder.Property(p => p.MetaDescription)
            .HasMaxLength(160);

        builder.Property(p => p.Tags)
            .HasMaxLength(500);

        builder.Property(p => p.IsPublished)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.ViewCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        // 索引
        builder.HasIndex(p => p.Slug)
            .IsUnique()
            .HasDatabaseName("IX_Posts_Slug");

        builder.HasIndex(p => p.PublishedAt)
            .HasDatabaseName("IX_Posts_PublishedAt");

        builder.HasIndex(p => p.IsPublished)
            .HasDatabaseName("IX_Posts_IsPublished");

        builder.HasIndex(p => p.CreatedAt)
            .HasDatabaseName("IX_Posts_CreatedAt");
    }
}
