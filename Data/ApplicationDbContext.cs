using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data.Entities;
using dotnet_blog_bmad.Data.Configurations;

namespace dotnet_blog_bmad.Data;

/// <summary>
/// 應用程式資料庫上下文
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// 文章資料集
    /// </summary>
    public DbSet<Post> Posts => Set<Post>();

    /// <summary>
    /// 管理員資料集
    /// </summary>
    public DbSet<Admin> Admins => Set<Admin>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 套用所有 IEntityTypeConfiguration
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
    }

    /// <summary>
    /// 覆寫 SaveChanges 以自動更新時間戳記
    /// </summary>
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// 覆寫 SaveChangesAsync 以自動更新時間戳記
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// 自動更新 CreatedAt 和 UpdatedAt 時間戳記
    /// </summary>
    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Property("CreatedAt").CurrentValue is DateTime)
                {
                    entry.Property("CreatedAt").CurrentValue = now;
                }
            }

            if (entry.Property("UpdatedAt").CurrentValue is DateTime)
            {
                entry.Property("UpdatedAt").CurrentValue = now;
            }
        }
    }
}
