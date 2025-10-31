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
    /// 標籤資料集
    /// </summary>
    public DbSet<Tag> Tags { get; set; }

    public DbSet<Comment> Comments { get; set; }

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

        // 配置 Tag 實體
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Slug).IsRequired().HasMaxLength(50);
            entity.HasIndex(t => t.Slug).IsUnique();
            entity.Property(t => t.Color).HasMaxLength(20).HasDefaultValue("gray");
            entity.Property(t => t.CreatedAt).IsRequired();
        });

        // Configure Comment entity
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.AuthorName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.AuthorEmail).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Content).IsRequired().HasColumnType("text");
            entity.Property(c => c.CreatedAt).IsRequired();
            entity.Property(c => c.IsApproved).IsRequired().HasDefaultValue(false);

            // Foreign key to Post
            entity.HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.PostId)
            .HasDatabaseName("IX_Comments_PostId");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.CreatedAt)
            .HasDatabaseName("IX_Comments_CreatedAt");

        // 配置 Post-Tag 多對多關係
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity(j => j.ToTable("PostTags"));
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
            // Check if entity has CreatedAt property
            var createdAtProperty = entry.Metadata.FindProperty("CreatedAt");
            if (entry.State == EntityState.Added && createdAtProperty != null)
            {
                entry.Property("CreatedAt").CurrentValue = now;
            }

            // Check if entity has UpdatedAt property
            var updatedAtProperty = entry.Metadata.FindProperty("UpdatedAt");
            if (updatedAtProperty != null)
            {
                entry.Property("UpdatedAt").CurrentValue = now;
            }
        }
    }
}
