using Microsoft.EntityFrameworkCore;
using dotnet_blog_bmad.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// 註冊資料庫上下文
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// 自動建立資料庫結構 (僅在開發環境)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            // 使用 EnsureCreated() 在開發環境快速建立資料庫
            // 注意: 這會跳過 Migrations，不適合生產環境
            if (dbContext.Database.EnsureCreated())
            {
                app.Logger.LogInformation("資料庫已成功建立");
            }
            else
            {
                app.Logger.LogInformation("資料庫已存在");
            }
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "建立資料庫時發生錯誤");
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
