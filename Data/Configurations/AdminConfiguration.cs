using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_blog_bmad.Data.Entities;

namespace dotnet_blog_bmad.Data.Configurations;

/// <summary>
/// Admin 實體的 Fluent API 配置
/// </summary>
public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        // 資料表名稱
        builder.ToTable("Admins");

        // 主鍵
        builder.HasKey(a => a.Id);

        // 屬性配置
        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.DisplayName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        // 索引
        builder.HasIndex(a => a.Email)
            .IsUnique()
            .HasDatabaseName("IX_Admins_Email");

        builder.HasIndex(a => a.IsActive)
            .HasDatabaseName("IX_Admins_IsActive");
    }
}
